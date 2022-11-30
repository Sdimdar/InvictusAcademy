using CloudStorage.Application.Contracts;
using CloudStorage.Infrastructure.Persistence;
using CloudStorage.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudStorage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CloudStorageDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CloudStorageConnectionString")));

        services.AddScoped<ICloudStorageRepository, CloudStorageRepository>();

        return services;
    }
}