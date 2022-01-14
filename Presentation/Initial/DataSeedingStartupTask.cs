using JfService.Balance.Infrastructure.DataSeeding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Presentation.Initial
{
    /// <summary>
    /// Сидирование данных в базу данных во время исполнения.
    /// </summary>
    public class DataSeedingStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public DataSeedingStartupTask(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task Execute()
        {
            var propaganations = _serviceProvider.GetServices<IDataRuntimeSeeder>();
            foreach (var propaganation in propaganations)
                await propaganation.Seed();
        }
    }
}