using AdminGateway.MVC.Mappings;
using AdminGateway.MVC.Services;
using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using DataTransferLib.Mappings;
using Microsoft.OpenApi.Models;

namespace AdminGateway.MVC.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IGetUsers, GetUsers>(c => c.BaseAddress = new Uri(configuration["ApiSettings:IdentityUrl"]));
        services.AddHttpClient<IRequestService, RequestService>(c => c.BaseAddress = new Uri(configuration["ApiSettings:RequestUrl"]));
        return services;
    }
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdminGateway.MVC", Version = "v1" });
            c.EnableAnnotations();
        });
        return services;
    }
    
    public static IServiceCollection SetCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:8081").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        }));
        return services;
    }
    
    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DefaultResponseObjectProfile());
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper());
        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IAdminCreate, CreateAdmin>();
        services.AddTransient<IAdminService, AdminService>();
        services.AddTransient<IRequestService, RequestService>();
        return services;
    }
    
}