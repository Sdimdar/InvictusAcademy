using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Request.Application.Contracts;
using Request.Infrastructure.Persistence;
using Request.Infrastructure.Repositories;

namespace Request.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RequestDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("RequestConnectionString")));
        
        services.AddScoped<IRequestRepository, RequestRepository>();

        return services;
    }
}