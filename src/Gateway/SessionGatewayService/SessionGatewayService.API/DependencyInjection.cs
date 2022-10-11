﻿using Microsoft.OpenApi.Models;

namespace SessionGatewayService.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureSessionServices(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.Name = ".InvictusAcademy.Session";
            options.Cookie.IsEssential = true;
        });
        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity.API", Version = "v1" });
            c.EnableAnnotations();
        });
        return services;
    }

    public static IServiceCollection SetCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        }));
        return services;
    }
}
