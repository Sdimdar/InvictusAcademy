using InvictusAcademyApp.Infrastructures.Databases;
using Microsoft.EntityFrameworkCore;

namespace InvictusAcademyApp.AppExtensions;

public static class AppServiceExtensions
{
    public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvictusDbContext>(op => 
            op.UseNpgsql(configuration.GetConnectionString("ConnectionString")));
    }
}