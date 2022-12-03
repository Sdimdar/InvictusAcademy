using CommonStructures;
using GlobalExceptionHandler.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;
using System.Net;

namespace GlobalExceptionHandler.Handlers;

public class PostgresExceptionHandler : IExceptionHandler
{
    public Type ExceptionType { get; }
    private readonly ILogger<InvalidCastExceptionHandler> _logger;

    public PostgresExceptionHandler(ILogger<InvalidCastExceptionHandler> logger)
    {
        _logger = logger;
        ExceptionType = typeof(InvalidCastException);
    }

    public async Task HandleAsync(Exception exception, HttpContext context)
    {
        if (exception is PostgresException postgresException)
        {
            _logger.LogWarning($"{BussinesErrors.PostgresException.ToString()}: {exception.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var data = "Database exception: " + postgresException.Message;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}
