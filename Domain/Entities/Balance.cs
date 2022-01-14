using System;

namespace JfService.Balance.Domain.Entities
{
    public class Balance : IEntity
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор ЛС.
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Период начисления.
        /// </summary>
        public DateTime Period { get; set; }

        /// <summary>
        /// Начальное сальдо.
        /// </summary>
        public decimal InBalance { get; set; }

        /// <summary>
        /// Начислено за период.
        /// </summary>
        public decimal Calculation { get; set; }
    }
}