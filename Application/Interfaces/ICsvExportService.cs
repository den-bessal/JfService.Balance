using System.Collections.Generic;

namespace JfService.Balance.Application.Interfaces
{
    /// <summary>
    /// Сервис экспорта данных в формат CSV.
    /// </summary>
    public interface ICsvExportService
    {
        /// <summary>
        /// Выполняет сериализацию записей в строку, представляющую CSV.
        /// </summary>
        string Serialize<T>(IEnumerable<T> records);
    }
}