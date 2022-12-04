using FreeArticles.Application.Contracts;
using FreeArticles.Infrastructure.Persistence;
using FreeArticles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeArticles.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FreeArticleDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("FreeArticleConnectionString")));

        services.AddScoped<IFreeArticleRepository, FreeArticleRepository>();

        return services;
    }
}