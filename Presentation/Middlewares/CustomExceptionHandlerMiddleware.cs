using JfService.Balance.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Presentation.Models;
using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = StatusCodes.Status500InternalServerError;
            var exceptionModel = new ExceptionModel(exception.Message);
            var result = JsonSerializer.Serialize(exceptionModel);

            switch (exception)
            {
                case NotFoundException notFoundException:
                    code = StatusCodes.Status404NotFound;
                    exceptionModel.Detail = notFoundException.Detail;
                    result = JsonSerializer.Serialize(exceptionModel);
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}