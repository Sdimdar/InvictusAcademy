using System.Net;
using ExceptionHandlerMiddleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Npgsql;

namespace ExceptionHandlerMiddleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;    
    
    public ExceptionHandlerMiddleware(RequestDelegate next)    
    {    
        _next = next;    
    }    
    
    public async Task Invoke(HttpContext context)    
    {    
        try    
        {    
            await _next.Invoke(context);    
        }    
        catch (Exception ex)    
        {    
            await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);         
        }    
    }
    
    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
    {
        object? data = null;
        switch (exception)
        {
            case PostgresException postgresException:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                data = "Postgres data base exception: " + postgresException.Message;
                break;
            case InternalServiceException internalServiceException:
                context.Response.StatusCode = internalServiceException.StatusCode;
                data = "InternalServiceException: " + internalServiceException.Message;
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                data = exception.Message;
                break;
        }
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonConvert.SerializeObject(data));
    }
}