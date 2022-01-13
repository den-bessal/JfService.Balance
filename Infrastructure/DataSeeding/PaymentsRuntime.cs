using AutoMapper;
using JfService.Balance.Application.DbContexts;
using JfService.Balance.Infrastructure.Models;
using JfService.Balance.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JfService.Balance.Infrastructure.DataSeeding
{
    public class PaymentsRuntime : IDataRuntimeSeeder
    {
        private readonly IJfServiceDbContext context;
        private readonly IMapper mapper;
        private readonly JsonDataSeederOptions options;

        public PaymentsRuntime(IJfServiceDbContext context, IMapper mapper, IOptionsMonitor<JsonDataSeederOptions> optionsAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            options = optionsAccessor.Get("Payments");
        }

        public async Task Seed()
        {
            var jsonContent = await File.ReadAllTextAsync(options.FilePath);
            var jArr = JArray.Parse(jsonContent);
            var paymentDtos = JsonConvert.DeserializeObject<List<PaymentDto>>(jArr?.ToString());
            var payments = mapper.Map<List<Domain.Entities.Payment>>(paymentDtos);
            context.Payments.AddRange(payments);
            await context.SaveChangesAsync();
        }
    }
}