using AutoMapper;
using CloudStorage.Application.Mappings;
using DataTransferLib.Mappings;
using Microsoft.OpenApi.Models;
using NLog.Web;


namespace CloudStorage.API;

public static class DependencyInjection
{
    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CloudStorageMapping());
            cfg.AddProfile(new DefaultResponseObjectProfile());
        }).CreateMapper());
        return services;
    }

    public static IServiceCollection SetCorsPolicy(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:8082").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            policy.WithOrigins("http://162.55.57.43").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        }));
        if (environment.IsDevelopment() || environment.EnvironmentName == "Local")
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        }
        if (environment.IsProduction())
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
        }
        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CloudStorage.API", Version = "v1" });
            c.EnableAnnotations();
        });
        return services;
    }
    
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        return builder;
    }
}