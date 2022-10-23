using AutoMapper;
using DataTransferLib.Mappings;
using Microsoft.OpenApi.Models;
using SessionGatewayService.Application.Contracts;
using SessionGatewayService.Application.Mappings;
using SessionGatewayService.Infrastructure.Services;

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
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
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

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IUserService, UserService>(c => c.BaseAddress = new Uri(configuration["ApiSettings:IdentityUrl"]));
        services.AddHttpClient<IRequestService, RequestService>(c => c.BaseAddress = new Uri(configuration["ApiSettings:RequestUrl"]));
        return services;
    }

    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DefaultResponseObjectProfile());
            cfg.AddProfile(new UserProfile());
        }).CreateMapper());
        return services;
    }
}
