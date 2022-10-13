using AutoMapper;
using DataTransferLib.Mappings;
using Identity.Application.Mappings;
using Microsoft.OpenApi.Models;

namespace Identity.API;
public static class DependencyInjection
{
    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
            cfg.AddProfile(new DefaultResponceObjectProfile());
        }).CreateMapper());
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
}
