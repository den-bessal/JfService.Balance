using JfService.Balance.Application.Mapping;
using JfService.Balance.Application.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace JfService.Balance.Application.Usecases.Balances.ViewModels
{
    /// <summary>
    /// Элементы оборотной ведомости, сгруппированные по типу периода.
    /// </summary>
    public class BalanceSheetGroupItemViewModel : IMapFrom<BalanceSheetGroupItem>
    {
        /// <summary>
        /// Тип периода.
        /// </summary>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// Элементы оборотной ведомости.
        /// </summary>
        public ICollection<BalanceSheetItemViewModel> Items { get; set; }
    }
}