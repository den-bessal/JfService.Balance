using JfService.Balance.Application.Usecases.Balances.Queries.GetBalances;
using JfService.Balance.Application.Usecases.Balances.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BalanceSheetController : ControllerBase
    {
        private readonly IMediator mediator;

        public BalanceSheetController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/api/[controller]/GetBalances")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BalanceSheetViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionModel))]
        public async Task<IActionResult> GetBalances([FromQuery] long accountId)
        {
            var response = await mediator.Send(new GetBalancesQuery(accountId));
            return Ok(response);
        }
    }
}