using JfService.Balance.Application.Mapping;
using JfService.Balance.Infrastructure.Converters;
using Newtonsoft.Json;
using System;

namespace JfService.Balance.Infrastructure.Models
{
    public class BalanceDto : IMapTo<Domain.Entities.Balance>
    {

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("period")]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Period { get; set; }

        [JsonProperty("in_balance")]
        public decimal InBalance { get; set; }

        [JsonProperty("calculation")]
        public decimal Calculation { get; set; }
    }
}