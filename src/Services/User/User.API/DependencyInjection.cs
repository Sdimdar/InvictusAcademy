using AutoMapper;
using DataTransferLib.Mappings;
using User.Application.Mappings;
using Microsoft.OpenApi.Models;

namespace User.API;
public static class DependencyInjection
{
    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
            cfg.AddProfile(new DefaultResponseObjectProfile());
        }).CreateMapper());
        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "User.API", Version = "v1" });
            c.EnableAnnotations();
        });
        return services;
    }
}
