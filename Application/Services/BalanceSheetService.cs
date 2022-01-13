using JfService.Balance.Application.DbContexts;
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

                var payments = await context.Payments.AsNoTracking()
                                                     .Where(x => x.AccountId == accountId)
                                                     .ToListAsync(ct);

                // Группировка по году
                BalanceSheetGroupItem yearGroup = new() { PeriodType = PeriodType.Year };
                decimal lastClosingBalanceByYear = 0;

                var balanceGroupedByYear = balances.GroupBy(x => x.Period.Year);

                foreach (var balancesByYear in balanceGroupedByYear)
                {
                    var openingBalance = balancesByYear.First().InBalance;

                    if (openingBalance != lastClosingBalanceByYear)
                        openingBalance = lastClosingBalanceByYear;

                    var calculationSum = balancesByYear.Sum(x => x.Calculation);

                    var paymentsByPeriod = payments.Where(x => x.Date.Year == balancesByYear.Key);
                    var paidSum = paymentsByPeriod.Sum(x => x.Sum);

                    var closingBalance = openingBalance + calculationSum - paidSum;

                    yearGroup.Items.Add(new BalanceSheetItem() 
                    {
                        PeriodName = $"{balancesByYear.Key} год",
                        OpeningBalance = openingBalance,
                        CalculationSum = calculationSum,
                        PaidSum = paidSum,
                        ClosingBalance = closingBalance
                    });

                    lastClosingBalanceByYear = closingBalance;
                }

                // Группировка по крваталу
                BalanceSheetGroupItem quarterGroup = new() { PeriodType = PeriodType.Quarter };
                decimal lastClosingBalanceByQuarter = 0;

                foreach (var balancesByYear in balanceGroupedByYear)
                {
                    foreach (var balancesByQuarter in balancesByYear.GroupBy(x => x.Period.Quarter()))
                    {
                        var openingBalance = balancesByQuarter.First().InBalance;

                        if (openingBalance != lastClosingBalanceByQuarter)
                            openingBalance = lastClosingBalanceByQuarter;

                        var calculationSum = balancesByQuarter.Sum(x => x.Calculation);

                        var paymentsByPeriod = payments.Where(x => x.Date.Quarter() == balancesByQuarter.Key);
                        var paidSum = paymentsByPeriod.Sum(x => x.Sum);

                        var closingBalance = openingBalance + calculationSum - paidSum;

                        quarterGroup.Items.Add(new BalanceSheetItem()
                        {
                            PeriodName = $"{balancesByQuarter.Key}-й квартал {balancesByYear.Key} года",
                            OpeningBalance = openingBalance,
                            CalculationSum = calculationSum,
                            PaidSum = paidSum,
                            ClosingBalance = closingBalance
                        });

                        lastClosingBalanceByQuarter = closingBalance;
                    }
                }

                // Группировка по месяцу
                BalanceSheetGroupItem monthGroup = new() { PeriodType = PeriodType.Month };
                decimal lastClosingBalanceByMonth = 0;

                foreach (var balancesByYear in balanceGroupedByYear)
                {
                    foreach (var balancesByMonth in balancesByYear.GroupBy(b => b.Period.Month))
                    {
                        var openingBalance = balancesByMonth.First().InBalance;

                        if (openingBalance != lastClosingBalanceByMonth)
                            openingBalance = lastClosingBalanceByMonth;

                        var calculationSum = balancesByMonth.Sum(x => x.Calculation);

                        var paymentsByPeriod = payments.Where(x => x.Date.Month == balancesByMonth.Key);
                        var paidSum = paymentsByPeriod.Sum(x => x.Sum);

                        var closingBalance = openingBalance + calculationSum - paidSum;

                        var period = balancesByMonth.First().Period;
                        var periodName = $"{period.ToString("MMM", new CultureInfo("ru-Ru"))} {period.Year} года";

                        monthGroup.Items.Add(new BalanceSheetItem()
                        {
                            PeriodName = periodName,
                            OpeningBalance = openingBalance,
                            CalculationSum = calculationSum,
                            PaidSum = paidSum,
                            ClosingBalance = closingBalance
                        });

                        lastClosingBalanceByMonth = closingBalance;
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
