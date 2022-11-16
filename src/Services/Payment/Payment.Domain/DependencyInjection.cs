using Microsoft.Extensions.DependencyInjection;
using Payment.Domain.Services;

namespace Payment.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddSingleton<PaymentService>();
        return services;
    }
}