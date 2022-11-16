using System.Net;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GlobalExceptionHandler;

internal class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ExceptionHandlerOptions _options;
    
    public ExceptionHandlerMiddleware(RequestDelegate next, ExceptionHandlerOptions options)    
    {    
        _next = next;
        _options = options;
    }    
    
    public async Task Invoke(HttpContext context, IServiceProvider provider)    
    {    
        try    
        {    
            await _next.Invoke(context);    
        }    
        catch (Exception exception)    
        {
            if (_options.Handlers.TryGetValue(exception.GetType(), out Type? handlerType))
            {
                var handler = (IExceptionHandler)provider.GetRequiredService(handlerType);
                await handler.HandleAsync(exception, context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string? data = exception.Message;
                data = GetStackTrace(data, exception.InnerException);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
            }
        }    
    }

    private string GetStackTrace(string @string, Exception? innerException)
    {
        if (innerException is null)
        {
            return @string;
        }
        @string += innerException.Message;
        GetStackTrace(@string, innerException.InnerException);
        return @string;
    }
}