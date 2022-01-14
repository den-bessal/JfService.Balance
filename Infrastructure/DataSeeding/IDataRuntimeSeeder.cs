using System.Threading.Tasks;

namespace JfService.Balance.Infrastructure.DataSeeding
{
    /// <summary>
    /// Предоставляет методы сидирования данных в базу данных во время исполнения.
    /// </summary>
    public interface IDataRuntimeSeeder
    {
        /// <summary>
        /// Выполняет сидирование.
        /// </summary>
        /// <returns></returns>
        Task Seed();
    }
}