using AutoMapper;
using DataTransferLib.Mappings;
using Microsoft.OpenApi.Models;
using Payment.API.Mappings;
using Payment.Infrastructure.Mappings;

namespace Payment.API;

public static class DependencyInjection
{
    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DbMappingProfile());
            cfg.AddProfile(new DefaultResponseObjectProfile());
            cfg.AddProfile(new ApiMappingProfile());
        }).CreateMapper());
        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Courses.API", Version = "v1" });
            c.EnableAnnotations();
        });
        return services;
    }
}
