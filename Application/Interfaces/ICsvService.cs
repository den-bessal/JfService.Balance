namespace JfService.Balance.Application.Interfaces
{
    public interface ICsvService
    {
        string Serialize<T>(T obj);

        void Deserialize<T>(string s);
    }
}