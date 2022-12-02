using AutoMapper;
using Courses.API.Mappings;
using Courses.Application.Mappings;
using Courses.Domain.Options;
using DataTransferLib.Mappings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NLog.Web;

namespace Courses.API;
public static class DependencyInjection
{
    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            //cfg.AddCollectionMappers();
            cfg.AddProfile(new MappingProfile());
            cfg.AddProfile(new ApiMappingProfile());
            cfg.AddProfile(new DefaultResponseObjectProfile());
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

    public static IServiceCollection AddCourseOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var options = services.BuildServiceProvider().GetService<IOptions<CourseResultOptions>>();
        options.Value.CourseDayDuration = int.Parse(configuration.GetSection("CourseResultOptions:CourseDayDuration").Value);

        return services;
    }
    
    
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        return builder;
    }
}
