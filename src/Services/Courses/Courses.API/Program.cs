using Courses.API;
using Courses.Application;
using Courses.Infrastructure;
using GlobalExceptionHandler.Extensions;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;

    // Add services to the container.
    services.AddMvc();
    services.AddEndpointsApiExplorer();
    services.AddControllers();
    services.AddSwaggerConfiguration();
    services.AddExceptionHandlers();
    services.AddCourseOptions(builder.Configuration);

    // Add API services
    services.AddInfrastructureServices(builder.Configuration);
    services.AddApplicationServices();

    // Add Automapper maps
    services.SetAutomapperProfiles();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseGlobalExceptionHandler();
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}