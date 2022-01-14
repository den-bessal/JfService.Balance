using JfService.Balance.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.Usecases.Balances.Queries.GetCurrentDebt
{
    /// <summary>
    /// Запрос получения текущей задолженности.
    /// </summary>
    public class GetCurrentDebtQuery : IRequest<decimal>
    {
        /// <summary>
        /// Идентификатор ЛС.
        /// </summary>
        public long AccountId { get; }

        public GetCurrentDebtQuery(long accountId) => AccountId = accountId;

        private class Handler : IRequestHandler<GetCurrentDebtQuery, decimal>
        {
            private readonly ILogger<Handler> logger;
            private readonly IBalanceSheetService balanceSheetService;

            public Handler(ILogger<Handler> logger, IBalanceSheetService balanceSheetService)
            {
                this.logger = logger;
                this.balanceSheetService = balanceSheetService;
            }

            public async Task<decimal> Handle(GetCurrentDebtQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await balanceSheetService.GetCurrentDebtAsync(request.AccountId, cancellationToken);
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
