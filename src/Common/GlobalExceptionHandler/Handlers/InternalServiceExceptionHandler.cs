using System.Runtime.InteropServices.ComTypes;
using CommonStructures;
using GlobalExceptionHandler.Exceptions;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GlobalExceptionHandler.Handlers;

public class InternalServiceExceptionHandler : IExceptionHandler
{
    public Type ExceptionType { get; }
    private readonly ILogger<InternalServiceExceptionHandler> _logger;

    public InternalServiceExceptionHandler(ILogger<InternalServiceExceptionHandler> logger)
    {
        _logger = logger;
        ExceptionType = typeof(InvalidCastException);
    }

    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is InternalServiceException internalServiceException)
        {
            _logger.LogWarning($"{BussinesErrors.InternalServiceException.ToString()}: {exception.Message}");
            context.Response.StatusCode = internalServiceException.StatusCode;
            var data = "Internal service exception: " + internalServiceException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}