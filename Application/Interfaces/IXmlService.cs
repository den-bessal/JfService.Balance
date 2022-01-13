namespace JfService.Balance.Application.Interfaces
{
    public interface IXmlService
    {
        string Serialize<T>(T obj);

        void Deserialize<T>(string s);
    }
}