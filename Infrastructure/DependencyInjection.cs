using JfService.Balance.Application.DbContexts;
using JfService.Balance.Application.Extenions;
using JfService.Balance.Infrastructure.DataSeeding;
using JfService.Balance.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace JfService.Balance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JfServiceDbContext>();

            services.AddScoped<IJfServiceDbContext>(provider => provider.GetService<JfServiceDbContext>());

            services.Configure<JsonDataSeederOptions>("Balances", configuration.GetSection($"{nameof(JsonDataSeederOptions)}:Balances"));
            services.Configure<JsonDataSeederOptions>("Payments", configuration.GetSection($"{nameof(JsonDataSeederOptions)}:Payments"));

            var types = Assembly.GetEntryAssembly()
                                .GetWithReferenced()
                                .SelectMany(x => x.GetExportedTypes())
                                .Where(type => type.GetInterfaces()
                                                   .Any(i => i == typeof(IDataRuntimeSeeder)));

            foreach (var type in types)
                services.AddScoped(typeof(IDataRuntimeSeeder), type);

            return services;
        }
    }
}