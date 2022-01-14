using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace JfService.Balance.Application.Models
{
    /// <summary>
    /// Тип периода.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PeriodType
    {
        /// <summary>
        /// Год.
        /// </summary>
        [EnumMember(Value = "Год")]
        Year,

        /// <summary>
        /// Квартал.
        /// </summary>
        [EnumMember(Value = "Квартал")]
        Quarter,

        /// <summary>
        /// Месяц.
        /// </summary>
        [EnumMember(Value = "Месяц")]
        Month
    }
}