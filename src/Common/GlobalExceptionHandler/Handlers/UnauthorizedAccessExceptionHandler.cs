using System.Net;
using CommonStructures;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GlobalExceptionHandler.Handlers;

public class UnauthorizedAccessExceptionHandler : IExceptionHandler
{
    public Type ExceptionType { get; }
    private readonly ILogger<UnauthorizedAccessExceptionHandler> _logger;
    public UnauthorizedAccessExceptionHandler(ILogger<UnauthorizedAccessExceptionHandler> logger)
    {
        _logger = logger;
        ExceptionType = typeof(UnauthorizedAccessException);
    }
    
    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is UnauthorizedAccessException unauthorizedAccessException)
        {
            _logger.LogWarning($"{BussinesErrors.UnauthorizedAccessException.ToString()}: {exception.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var data= "Authorize exception: " + unauthorizedAccessException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}