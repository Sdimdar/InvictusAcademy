using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserGateway.Application.Contracts;
using UserGateway.Infrastructure.Services;

namespace UserGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisCacheConnectionString");
        });

        services.AddTransient<IPaymentService, PaymentService>();

        return services;
    }
}