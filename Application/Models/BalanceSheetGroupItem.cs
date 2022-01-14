using System.Collections.Generic;

namespace JfService.Balance.Application.Models
{
    /// <summary>
    /// Элементы оборотной ведомости, сгруппированные по типу периода.
    /// </summary>
    public class BalanceSheetGroupItem
    {
        /// <summary>
        /// Тип периода.
        /// </summary>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// Элементы оборотной ведомости.
        /// </summary>
        public IList<BalanceSheetItem> Items { get; set; } = new List<BalanceSheetItem>();
    }
}