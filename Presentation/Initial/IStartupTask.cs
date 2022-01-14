using System.Threading.Tasks;

namespace Presentation.Initial
{
    /// <summary>
    /// Предоставляет методы запуска задачи, запускаемой перед запуском приложения.
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// Выполяет задачу.
        /// </summary>
        Task Execute();
    }
}