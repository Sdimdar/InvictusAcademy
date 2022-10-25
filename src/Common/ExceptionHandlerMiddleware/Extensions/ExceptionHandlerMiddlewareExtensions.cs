using Microsoft.AspNetCore.Builder;

namespace ExceptionHandlerMiddleware.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ExceptionHandlerMiddleware>();  
    }  
}