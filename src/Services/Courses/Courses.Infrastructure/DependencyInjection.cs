using CommonRepository.Models;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using Courses.Domain.Entities.CourseResults;
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
            Dictionary<Type, string> settings = new()
            {
                { typeof(CourseInfoDbModel), "Courses" },
                { typeof(ModuleInfoDbModel), "Modules" },
                { typeof(CourseResultInfoDbModel), "CourseResults" }
            };
            options.CollectionNames = settings;
            options.DatabaseName = configuration.GetSection("InvictusAcademyDatabase:DatabaseName").Value;
        });
        services.AddSingleton<ICourseInfoRepository, CourseInfosRepository>();
        services.AddSingleton<IModuleInfoRepository, ModuleInfoRepository>();
        services.AddSingleton<ICourseResultsInfoRepository, CourseResultsInfoRepository>();
        services.AddDbContext<CoursesDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CoursesConnectionString")));
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICoursePurchasedRepository, CoursePurchasedRepository>();
        services.AddScoped<ICourseWishedRepository, CourseWishedRepository>();
        return services;
    }
}