using CsvHelper;
using CsvHelper.Configuration;
using JfService.Balance.Application.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace JfService.Balance.Infrastructure.Services
{
    public class CsvExportService : ICsvExportService
    {
        public string Serialize<T>(IEnumerable<T> records)
        {
            using var stringWriter = new StringWriter();
            using var csvWriter = new CsvWriter(stringWriter, new CsvConfiguration(CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(records);
            return stringWriter.ToString();
        }
    }
}