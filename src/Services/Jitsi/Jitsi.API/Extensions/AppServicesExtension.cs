using AutoMapper;
using DataTransferLib.Mappings;
using Jitsi.API.Mappings;
using Jitsi.API.Models;
using Jitsi.API.Repostories;
using Jitsi.API.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace Jitsi.API.Extensions;

public static class AppServicesExtension
{
    public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StreamingRoomDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("StreamingRoomConnectionString")));
        services.AddScoped<IStreamingRoomRepository, StreamingRoomRepository>();
    }
    
    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        return builder;
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
}