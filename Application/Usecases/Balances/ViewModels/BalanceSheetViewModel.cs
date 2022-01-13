using JfService.Balance.Application.Mapping;
using JfService.Balance.Application.Models;
using System.Collections.Generic;

namespace JfService.Balance.Application.Usecases.Balances.ViewModels
{
    /// <summary>
    /// Оборотная ведомость.
    /// </summary>
    public class BalanceSheetViewModel : List<BalanceSheetGroupItemViewModel>, IMapFrom<BalanceSheet>
    {
    }
}