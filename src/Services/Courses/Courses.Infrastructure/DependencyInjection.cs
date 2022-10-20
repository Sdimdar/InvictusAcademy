using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<InvictusProjectDatabaseSettings>(options => 
        {
            options.ConnectionString = configuration.GetSection("InvictusAcademy:ConnectionString").Value;
            options.CollectionName = configuration.GetSection("InvictusAcademy:CollectionName").Value;
            options.DatabaseName = configuration.GetSection("InvictusAcademy:DatabaseName").Value;
        } );
        services.AddSingleton<ICoursesRepository, CoursesRepository>();
        return services;
    }
}