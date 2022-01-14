using System;

namespace JfService.Balance.Domain.Entities
{
    /// <summary>
    /// Интерфейс, описывающий базовые атрибуты сущности.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public Guid Id { get; set; }
    }
}