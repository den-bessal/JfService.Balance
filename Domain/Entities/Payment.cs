using System;

namespace JfService.Balance.Domain.Entities
{
    public class Payment : IEntity
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор ЛС.
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Дата платежа.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма платежа.
        /// </summary>
        public decimal Sum { get; set; }
    }
}