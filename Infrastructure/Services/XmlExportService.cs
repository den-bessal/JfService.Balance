using JfService.Balance.Application.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace JfService.Balance.Infrastructure.Services
{
    public class XmlExportService : IXmlExportService
    {
        public string Serialize<T>(T obj)
        {
            using var writer = new StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }
}