using AdminGateway.MVC.Mappings;
using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using AdminGateway.MVC.Services;
using AdminGateway.MVC.Services.Interfaces;
using AutoMapper;
using DataTransferLib.Mappings;
using ExtendedHttpClient.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AdminGateway.MVC;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExtendedHttpClient();
        services.AddServiceWithExtendedHttpClient<IRequestService, RequestService>(
            configuration["ApiSettings:RequestUrl"]);
        services.AddServiceWithExtendedHttpClient<IGetUsers, GetUsers>(configuration["ApiSettings:IdentityUrl"]);
        services.AddServiceWithExtendedHttpClient<ICoursesService, CoursesService>(configuration["ApiSettings:CourseUrl"]);
        services.AddServiceWithExtendedHttpClient<IModulesService, ModulesService>(configuration["ApiSettings:CourseUrl"]);
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

    public static IServiceCollection SetCorsPolicy(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:8082").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            policy.WithOrigins("http://162.55.57.43:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        }));
        if (environment.IsDevelopment() || environment.EnvironmentName == "Local")
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        }
        if (environment.IsProduction())
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
        }

        return services;
    }

    public static IServiceCollection SetAutomapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DefaultResponseObjectProfile());
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper());
        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IAdminService, AdminService>();
        services.AddTransient<IRequestService, RequestService>();
        services.AddTransient<IGetUsers, GetUsers>();
        services.AddTransient<ICoursesService, CoursesService>();
        services.AddTransient<IModulesService, ModulesService>();
        return services;
    }

    public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AdminDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AdminConnection")));
        services.AddIdentity<AdminUser, IdentityRole>().AddEntityFrameworkStores<AdminDbContext>();
        return services;
    }
}