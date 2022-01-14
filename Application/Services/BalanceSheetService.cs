using JfService.Balance.Application.DbContexts;
using JfService.Balance.Application.Exceptions;
using JfService.Balance.Application.Extenions;
using JfService.Balance.Application.Interfaces;
using JfService.Balance.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.Services
{
    public class BalanceSheetService : IBalanceSheetService
    {
        private readonly ILogger<BalanceSheetService> logger;
        private readonly IJfServiceDbContext context;

        public BalanceSheetService(ILogger<BalanceSheetService> logger, IJfServiceDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<BalanceSheet> GetAsync(long accountId, CancellationToken ct = default)
        {
            try
            {
                var balances = await context.Balances.AsNoTracking()
                                                     .Where(x => x.AccountId == accountId)
                                                     .OrderBy(x => x.Period)
                                                     .ToListAsync(ct);

                if (!balances.Any())
                    throw new NotFoundException("Object not found", $"Для ЛС {accountId} не обнаружено ни одной записи");

                var payments = await context.Payments.AsNoTracking()
                                                     .Where(x => x.AccountId == accountId)
                                                     .ToListAsync(ct);

                BalanceSheetGroupItem yearGroup = new() { PeriodType = PeriodType.Year };
                BalanceSheetGroupItem quarterGroup = new() { PeriodType = PeriodType.Quarter };
                BalanceSheetGroupItem monthGroup = new() { PeriodType = PeriodType.Month };

                var balanceGroupedByYear = balances.GroupBy(x => x.Period.Year);
                decimal lastClosingBalanceByYear, lastClosingBalanceByQuarter, lastClosingBalanceByMonth;
                
                lastClosingBalanceByYear 
                    = lastClosingBalanceByQuarter 
                    = lastClosingBalanceByMonth
                    = balanceGroupedByYear.First()
                                          .First().InBalance;

                // По годам
                foreach (var balancesByYear in balanceGroupedByYear)
                {
                    var openingBalanceByYear = balancesByYear.First().InBalance;

                    if (openingBalanceByYear != lastClosingBalanceByYear)
                        openingBalanceByYear = lastClosingBalanceByYear;

                    var calculationSumByYear = balancesByYear.Sum(x => x.Calculation);

                    var paymentsByYear = payments.Where(x => x.Date.Year == balancesByYear.Key);
                    var paidSumByYear = paymentsByYear.Sum(x => x.Sum);

                    var closingBalanceByYear = openingBalanceByYear + calculationSumByYear - paidSumByYear;

                    yearGroup.Items.Add(new BalanceSheetItem() 
                    {
                        PeriodName = $"{balancesByYear.Key} год",
                        OpeningBalance = openingBalanceByYear,
                        CalculationSum = calculationSumByYear,
                        PaidSum = paidSumByYear,
                        ClosingBalance = closingBalanceByYear
                    });

                    lastClosingBalanceByYear = closingBalanceByYear;

                    // По кварталам
                    foreach (var balancesByQuarter in balancesByYear.GroupBy(x => x.Period.Quarter()))
                    {
                        var openingBalanceByQuarter = balancesByQuarter.First().InBalance;

                        if (openingBalanceByQuarter != lastClosingBalanceByQuarter)
                            openingBalanceByQuarter = lastClosingBalanceByQuarter;

                        var calculationSumQuarter = balancesByQuarter.Sum(x => x.Calculation);

                        var paymentsByQuarter = payments.Where(x => x.Date.Year == balancesByYear.Key && x.Date.Quarter() == balancesByQuarter.Key);
                        var paidSumByQuarter = paymentsByQuarter.Sum(x => x.Sum);

                        var closingBalanceByQuarter = openingBalanceByQuarter + calculationSumQuarter - paidSumByQuarter;

                        quarterGroup.Items.Add(new BalanceSheetItem()
                        {
                            PeriodName = $"{balancesByQuarter.Key}-й квартал {balancesByYear.Key} года",
                            OpeningBalance = openingBalanceByQuarter,
                            CalculationSum = calculationSumQuarter,
                            PaidSum = paidSumByQuarter,
                            ClosingBalance = closingBalanceByQuarter
                        });

                        lastClosingBalanceByQuarter = closingBalanceByQuarter;

                        // По месяцам
                        foreach (var balancesByMonth in balancesByQuarter.GroupBy(b => b.Period.Month))
                        {
                            var openingBalanceByMonth = balancesByMonth.First().InBalance;

                            if (openingBalanceByMonth != lastClosingBalanceByMonth)
                                openingBalanceByMonth = lastClosingBalanceByMonth;

                            var calculationSumByMonth = balancesByMonth.Sum(x => x.Calculation);

                            var paymentsByMonth = payments.Where(x => x.Date.Year == balancesByYear.Key && x.Date.Month == balancesByMonth.Key);
                            var paidSumByMonth = paymentsByMonth.Sum(x => x.Sum);

                            var closingBalanceByMonth = openingBalanceByMonth + calculationSumByMonth - paidSumByMonth;

                            var period = balancesByMonth.First().Period;
                            var periodName = $"{period.ToString("MMMM", new CultureInfo("ru-Ru"))} {period.Year} года";

                            monthGroup.Items.Add(new BalanceSheetItem()
                            {
                                PeriodName = periodName,
                                OpeningBalance = openingBalanceByMonth,
                                CalculationSum = calculationSumByMonth,
                                PaidSum = paidSumByMonth,
                                ClosingBalance = closingBalanceByMonth
                            });

                            lastClosingBalanceByMonth = closingBalanceByMonth;
                        }
                    }
                }

                return new BalanceSheet() 
                {
                    yearGroup,
                    quarterGroup,
                    monthGroup
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
