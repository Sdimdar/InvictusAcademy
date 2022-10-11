using Community.Microsoft.Extensions.Caching.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SessionGatewayService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedPostgreSqlCache(options =>
        {
            options.ConnectionString = configuration.GetConnectionString("SessionConnectionString");
            options.SchemaName = configuration["SchemaName"];
            options.TableName = configuration["TableName"];
            options.CreateInfrastructure = true;
        });

        return services;
    }
}