using AdminGateway.MVC.Mappings;
using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services;
using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using ExtendedHttpClient.Extensions;
using ExtendedHttpClient.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AdminGateway.MVC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExtendedHttpClient();
            services.AddServiceWithExtendedHttpClient<IRequestService, RequestService>(
                configuration["ApiSettings:RequestUrl"]);
            services.AddServiceWithExtendedHttpClient<IGetUsers, GetUsers>(configuration["ApiSettings:IdentityUrl"]);
            return services;
        }
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdminGateway.MVC", Version = "v1" });
                c.EnableAnnotations();
            });
            return services;
        }
    
        public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
        {
        
            services.AddSingleton(_ => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper());
            return services;
        }
    
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IAdminCreate, CreateAdmin>();
            services.AddTransient<IRequestService, RequestService>();
            return services;
        }

        public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdminDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AdminConnection")));
            services.AddIdentity<AdminUser, IdentityRole>().AddEntityFrameworkStores<AdminDbContext>();
            return services;
        }
        
    
    }
}