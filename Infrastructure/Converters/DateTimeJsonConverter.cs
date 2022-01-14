using Newtonsoft.Json;
using System;
using System.Globalization;

namespace JfService.Balance.Infrastructure.Converters
{
    public class DateTimeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(DateTime);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => DateTime.ParseExact(reader.Value?.ToString(), "yyyyMM", CultureInfo.InvariantCulture);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => writer.WriteValue(((DateTime)value).ToString("yyyyMM", CultureInfo.InvariantCulture));
    }
}