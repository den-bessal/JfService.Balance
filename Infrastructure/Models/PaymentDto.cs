using JfService.Balance.Application.Mapping;
using JfService.Balance.Domain.Entities;
using Newtonsoft.Json;
using System;

namespace JfService.Balance.Infrastructure.Models
{
    public class PaymentDto : IMapTo<Payment>
    {
        [JsonProperty("payment_guid")]
        public Guid Id { get; set; }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("sum")]
        public decimal Sum { get; set; }
    }
}