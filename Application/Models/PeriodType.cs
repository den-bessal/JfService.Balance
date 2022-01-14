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
        [EnumMember]
        Year,

        /// <summary>
        /// Квартал.
        /// </summary>
        [EnumMember]
        Quarter,

        /// <summary>
        /// Месяц.
        /// </summary>
        [EnumMember]
        Month
    }
}