using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Infrastructure.Persistance;
using Courses.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Courses.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<InvictusProjectDatabaseSettings>(options => 
        {
            options.ConnectionString = configuration.GetSection("InvictusAcademyDatabase:ConnectionString").Value;
            options.CollectionName = configuration.GetSection("InvictusAcademyDatabase:CollectionName").Value;
            options.DatabaseName = configuration.GetSection("InvictusAcademyDatabase:DatabaseName").Value;
        } );
        services.AddSingleton<ICourseInfosRepository, CourseInfosRepository>();
        services.AddDbContext<CoursesDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CoursesConnectionString")));
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICoursePurchasedRepository, CoursePurchasedRepository>();
        services.AddScoped<ICourseWishedRepository, CourseWishedRepository>();
        return services;
    }
}