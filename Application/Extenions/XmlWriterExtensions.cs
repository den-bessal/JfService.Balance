using System;
using System.Xml;

namespace JfService.Balance.Application.Extenions
{
    public static class XmlWriterExtensions
    {
        public static void WriteProperty(this XmlWriter writer, string name, object value)
        {
            writer.WriteStartElement(name);
            writer.WriteString(value.ToString());
            writer.WriteEndElement();
        }

        public static void WriteElement(this XmlWriter writer, string name, Action writeBodyAction)
        {
            writer.WriteStartElement(name);
            writeBodyAction.Invoke();
            writer.WriteEndElement();
        }
    }
}