namespace Presentation.Models
{
    /// <summary>
    /// Модель описания исключения.
    /// </summary>
    public class ExceptionModel
    {
        public ExceptionModel(string errorMessage, string detail)
        {
            ErrorMessage = errorMessage;
            Detail = detail;
        }

        public ExceptionModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Detail = "При выполнении операции произошла ошибка";
        }

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Описание ошибки.
        /// </summary>
        public string Detail { get; set; }
    }
}