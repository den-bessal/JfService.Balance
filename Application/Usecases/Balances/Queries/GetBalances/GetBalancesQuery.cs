using AutoMapper;
using JfService.Balance.Application.Interfaces;
using JfService.Balance.Application.Usecases.Balances.ViewModels;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.Usecases.Balances.Queries.GetBalances
{
    /// <summary>
    /// Запрос получения оборотной ведомости.
    /// </summary>
    public class GetBalancesQuery : IRequest<BalanceSheetViewModel>
    {
        /// <summary>
        /// Идентификатор ЛС.
        /// </summary>
        public long AccountId { get; }

        public GetBalancesQuery(long accountId) => AccountId = accountId;

        private class Handler : IRequestHandler<GetBalancesQuery, BalanceSheetViewModel>
        {
            private readonly IMapper mapper;
            private readonly ILogger<Handler> logger;
            private readonly IBalanceSheetService balanceSheetService;

            public Handler(IMapper mapper, ILogger<Handler> logger, IBalanceSheetService balanceSheetService)
            {
                this.mapper = mapper;
                this.logger = logger;
                this.balanceSheetService = balanceSheetService;
            }

            public async Task<BalanceSheetViewModel> Handle(GetBalancesQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var balanceSheet = await balanceSheetService.GetBalanceSheetAsync(request.AccountId, cancellationToken);
                    return mapper.Map<BalanceSheetViewModel>(balanceSheet);
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