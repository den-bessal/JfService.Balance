using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Initial;
using System.Threading.Tasks;

namespace Presentation.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        /// Выполняет запуск задач <see cref="IStartupTask"/> перед запуском приложения.
        /// </summary>
        public static async Task RunStartupTasks(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var startupTasks = scope.ServiceProvider.GetServices<IStartupTask>();
            foreach (var task in startupTasks)
                await task.Execute();
        }
    }
}