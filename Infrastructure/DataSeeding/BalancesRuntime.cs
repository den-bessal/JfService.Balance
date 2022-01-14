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
    public class BalancesRuntime : IDataRuntimeSeeder
    {
        private readonly IJfServiceDbContext context;
        private readonly IMapper mapper;
        private readonly JsonDataSeederOptions options;

        public BalancesRuntime(IJfServiceDbContext context, IMapper mapper, IOptionsMonitor<JsonDataSeederOptions> optionsAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            options = optionsAccessor.Get("Balances");
        }

        public async Task Seed()
        {
            var jsonContent = await File.ReadAllTextAsync(options.FilePath);
            var jObj = JObject.Parse(jsonContent);
            var balanceDtos = JsonConvert.DeserializeObject<List<BalanceDto>>(jObj["balance"]?.ToString());
            var balances = mapper.Map<List<Domain.Entities.Balance>>(balanceDtos);
            context.Balances.RemoveRange(context.Balances); // TODO: Убрать этот костыль!!!
            context.Balances.AddRange(balances);
            await context.SaveChangesAsync();
        }
    }
}