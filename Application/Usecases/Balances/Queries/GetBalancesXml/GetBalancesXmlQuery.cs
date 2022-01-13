using JfService.Balance.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.Usecases.Balances.Queries.GetBalancesXml
{
    /// <summary>
    /// Запрос получения оборотной ведомости в формате XML.
    /// </summary>
    public class GetBalancesXmlQuery : IRequest<string>
    {
        /// <summary>
        /// Идентификатор ЛС.
        /// </summary>
        public long AccountId { get; set; }

        private class Handler : IRequestHandler<GetBalancesXmlQuery, string>
        {
            private readonly ILogger<Handler> logger;
            private readonly IBalanceSheetService balanceSheetService;
            private readonly IXmlService xmlService;

            public Handler(ILogger<Handler> logger, IBalanceSheetService balanceSheetService, IXmlService xmlService)
            {
                this.logger = logger;
                this.balanceSheetService = balanceSheetService;
                this.xmlService = xmlService;
            }

            public async Task<string> Handle(GetBalancesXmlQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var balanceSheet = await balanceSheetService.GetAsync(request.AccountId, cancellationToken);
                    return xmlService.Serialize(balanceSheet);
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