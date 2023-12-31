using GlobalExceptionHandler.Extensions;
using NLog;
using NLog.Web;
using User.API;
using User.Application;
using User.Infrastructure;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddLogging();
    var services = builder.Services;

    // Add services to the container.
    services.AddMvc();
    services.AddEndpointsApiExplorer();
    services.AddControllers().AddNewtonsoftJson();
    services.AddSwaggerConfiguration();
    services.AddExceptionHandlers();

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
public partial class Program { }
