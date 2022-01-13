namespace JfService.Balance.Application.Interfaces
{
    /// <summary>
    /// Сервис экспорта данных в формат XML.
    /// </summary>
    public interface IXmlExportService
    {
        /// <summary>
        /// Выполняет сериализацию объекта в строку, представляющую XML.
        /// </summary>
        string Serialize<T>(T obj);
    }
}