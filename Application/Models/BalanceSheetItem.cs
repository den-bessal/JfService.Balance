namespace JfService.Balance.Application.Models
{
    /// <summary>
    /// Элемент оборотной ведомости.
    /// </summary>
    public class BalanceSheetItem
    {
        /// <summary>
        /// Наименование периода.
        /// </summary>
        [CsvHelper.Configuration.Attributes.Name("Наименование периода")]
        public string PeriodName { get; set; }

        /// <summary>
        /// Входящее сальдо на начало периода.
        /// </summary>
        [CsvHelper.Configuration.Attributes.Name("Входящее сальдо на начало периода")]
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// Начислено за период.
        /// </summary>
        [CsvHelper.Configuration.Attributes.Name("Начислено за период")]
        public decimal CalculationSum { get; set; }

        /// <summary>
        /// Оплачено за период.
        /// </summary>
        [CsvHelper.Configuration.Attributes.Name("Оплачено за период")]
        public decimal PaidSum { get; set; }

        /// <summary>
        /// Исходящий баланс на конец периода.
        /// </summary>
        [CsvHelper.Configuration.Attributes.Name("Исходящий баланс на конец периода")]
        public decimal ClosingBalance { get; set; }
    }
}