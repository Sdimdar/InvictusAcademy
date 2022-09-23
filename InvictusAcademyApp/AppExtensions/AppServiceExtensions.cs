using InvictusAcademyApp.Infrastructures.Databases;
using InvictusAcademyApp.Models.DbModels;
using InvictusAcademyApp.Services;
using InvictusAcademyApp.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InvictusAcademyApp.AppExtensions;

public static class AppServiceExtensions
{
    public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvictusDbContext>(op => 
            op.UseNpgsql(configuration.GetConnectionString("ConnectionString")))
            .AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<InvictusDbContext>();;
        services.AddTransient<IAccountService, AccountService>();
    }
}