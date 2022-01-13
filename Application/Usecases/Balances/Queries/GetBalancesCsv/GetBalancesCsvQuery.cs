using JfService.Balance.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.Usecases.Balances.Queries.GetBalancesCsv
{
    /// <summary>
    /// Запрос получения оборотной ведомости в формате CSV.
    /// </summary>
    public class GetBalancesCsvQuery : IRequest<string>
    {
        /// <summary>
        /// Идентификатор ЛС.
        /// </summary>
        public long AccountId { get; set; }

        private class Handler : IRequestHandler<GetBalancesCsvQuery, string>
        {
            private readonly ILogger<Handler> logger;
            private readonly IBalanceSheetService balanceSheetService;
            private readonly ICsvExportService csvService;

            public Handler(ILogger<Handler> logger, IBalanceSheetService balanceSheetService, ICsvExportService csvService)
            {
                this.logger = logger;
                this.balanceSheetService = balanceSheetService;
                this.csvService = csvService;
            }

            public async Task<string> Handle(GetBalancesCsvQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var balanceSheet = await balanceSheetService.GetAsync(request.AccountId, cancellationToken);
                    return csvService.Serialize(balanceSheet.SelectMany(x => x.Items));
                }
                catch (Exception e)
                {
                    logger.LogError(e, e.Message);
                    throw;
                }
            }
        }
    }
}