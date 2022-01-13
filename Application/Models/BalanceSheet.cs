using JfService.Balance.Application.Extenions;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace JfService.Balance.Application.Models
{
    /// <summary>
    /// Оборотная ведомость.
    /// </summary>
    [XmlRoot("ОборотнаяВедомость")]
    public class BalanceSheet : List<BalanceSheetGroupItem>, IXmlSerializable
    {
        public XmlSchema GetSchema() => null;

        public void ReadXml(XmlReader reader) => throw new NotImplementedException();

        public void WriteXml(XmlWriter writer)
        {
            void WriteRecords(string groupName, IEnumerable<BalanceSheetItem> records)
            {
                writer.WriteElement(groupName, () =>
                {
                    foreach (var record in records)
                    {
                        writer.WriteElement("ЗаписьБаланса", () =>
                        {
                            writer.WriteProperty("Период", record.PeriodName);
                            writer.WriteProperty("ВходСальдоНаНачПериода", record.OpeningBalance);
                            writer.WriteProperty("НачисленоЗаПериод", record.CalculationSum);
                            writer.WriteProperty("ОплаченоЗаПериод", record.PaidSum);
                            writer.WriteProperty("ИсхБалансНаКонецПериода", record.ClosingBalance);
                        });
                    }
                });
            }

            foreach (var item in this)
            {
                switch (item.PeriodType)
                {
                    case PeriodType.Year:
                        WriteRecords("ПоГодам", item.Items);
                        break;

                    case PeriodType.Quarter:
                        WriteRecords("ПоКварталам", item.Items);
                        break;

                    case PeriodType.Month:
                        WriteRecords("ПоМесяцам", item.Items);
                        break;
                }
            }
        }
    }
}