using JfService.Balance.Application.Mapping;
using JfService.Balance.Application.Models;

namespace JfService.Balance.Application.Usecases.Balances.ViewModels
{
    /// <summary>
    /// Элемент оборотной ведомости.
    /// </summary>
    public class BalanceSheetItemViewModel : IMapFrom<BalanceSheetItem>
    {
        /// <summary>
        /// Наименование периода.
        /// </summary>
        public string PeriodName { get; set; }

        /// <summary>
        /// Входящее сальдо на начало периода.
        /// </summary>
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// Начислено за период.
        /// </summary>
        public decimal CalculationSum { get; set; }

        /// <summary>
        /// Оплачено за период.
        /// </summary>
        public decimal PaidSum { get; set; }

        /// <summary>
        /// Исходящий баланс на конец периода.
        /// </summary>
        public decimal ClosingBalance { get; set; }
    }
}