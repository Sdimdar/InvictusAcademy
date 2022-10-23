using Admin.MVC.Services;
using Admin.MVC.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace Admin.MVC.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IRequestService, RequestService>(c => c.BaseAddress = new Uri(configuration["ApiSettings:RequestUrl"]));
        return services;
    }
}