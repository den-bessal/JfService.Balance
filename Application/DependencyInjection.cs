using FluentValidation;
using JfService.Balance.Application.Behaviors;
using JfService.Balance.Application.Extenions;
using JfService.Balance.Application.Interfaces;
using JfService.Balance.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JfService.Balance.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetEntryAssembly().GetWithReferenced());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IBalanceSheetService, BalanceSheetService>();

            return services;
        }
    }
}