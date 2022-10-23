using AdminGateway.MVC.Services;
using AdminGateway.MVC.Services.Interfaces;

namespace AdminGateway.MVC.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IRequestService, RequestService>(c => c.BaseAddress = new Uri(configuration["ApiSettings:RequestUrl"]));
        return services;
    }
}