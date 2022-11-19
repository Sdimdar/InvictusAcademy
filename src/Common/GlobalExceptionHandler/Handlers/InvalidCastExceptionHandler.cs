using System.Net;
using CommonStructures;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GlobalExceptionHandler.Handlers;

public class InvalidCastExceptionHandler : IExceptionHandler
{
    private readonly ILogger<InvalidCastExceptionHandler> _logger;
    public Type ExceptionType { get; }
    
    public InvalidCastExceptionHandler(ILogger<InvalidCastExceptionHandler> logger)
    {
        _logger = logger;
        ExceptionType = typeof(InvalidCastException);
    }
    
    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is InvalidCastException invalidCastException)
        {
            _logger.LogWarning($"{BussinesErrors.InvalidCastException.ToString()}: {exception.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var data= "Invalid type cast exception: " + invalidCastException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}