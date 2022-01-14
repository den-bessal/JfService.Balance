namespace JfService.Balance.Infrastructure.Options
{
    /// <summary>
    /// Опции сидера времени исполнения, использующего файл JSON как входные данные.
    /// </summary>
    public class JsonDataSeederOptions
    {
        /// <summary>
        /// Путь к файлу.
        /// </summary>
        public string FilePath { get; set; }
    }
}