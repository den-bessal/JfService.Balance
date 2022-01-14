using System;

namespace JfService.Balance.Application.Exceptions
{
    /// <summary>
    /// Исключение для статуса 404.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Детали ошибки.
        /// </summary>
        public string Detail { get; } = "Запрашиваемый объект не найден";

        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, string detail) : base(message) => Detail = detail;
    }
}