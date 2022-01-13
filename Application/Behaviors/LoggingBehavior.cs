using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace JfService.Balance.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
            => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = new Stopwatch();
            var requestTypeName = typeof(TRequest).Name;

            logger.LogInformation("Выполняется запрос {requestTypeName}", requestTypeName);

            try
            {
                stopWatch.Start();
                logger.LogInformation("Тело запроса: {@request}", request);
            }
            finally
            {
                stopWatch.Stop();
                logger.LogInformation("Запрос {requestTypeName} выполнен за {ElapsedMilliseconds} мс", requestTypeName, stopWatch.ElapsedMilliseconds);
            }

            return await next();
        }
    }
}