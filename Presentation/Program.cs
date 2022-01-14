using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Presentation.Extensions;
using Serilog;
using Serilog.Enrichers.Span;
using System;
using System.Threading.Tasks;

namespace Presentation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Log.Information("Starting up");
                var host = CreateHostBuilder(args).Build();
                await host.RunStartupTasks();
                await host.RunAsync();
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) 
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                          .AddJsonFile($"appsettings.{environment}.json", optional: true).Build();

            var hostBuilder = Host.CreateDefaultBuilder(args)
                                  .ConfigureWebHostDefaults(webBuilder => 
                                  {
                                      webBuilder.UseStartup<Startup>();
                                  });

            hostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.Enrich.WithExceptionStackTraceHash()
                                   .Enrich.WithSpan()
                                   .WriteTo.Console()
                                   .Enrich.WithProperty("Environment", environment)
                                   .ReadFrom.Configuration(configuration);
            });

            return hostBuilder;
        }
    }
}
