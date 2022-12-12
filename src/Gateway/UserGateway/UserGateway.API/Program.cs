using GlobalExceptionHandler.Extensions;
using NLog;
using NLog.Web;
using UserGateway.API;
using UserGateway.Application;
using UserGateway.Infrastructure;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services;

    // Add services to the container.
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerConfiguration();
    services.ConfigureSessionServices(builder.Environment);
    services.SetAutomapperProfiles();
    services.AddExceptionHandlers();

    // Add API services
    services.AddInfrastructureServices(builder.Configuration);
    services.AddApplicationServices();
    services.AddHttpClients(builder.Configuration);

    // Configure CORS Policy and Cookie
    services.SetCorsPolicy();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseGlobalExceptionHandler();
    app.UseHttpsRedirection();
    app.UseCors("CorsPolicy");

    app.UseAuthorization();
    app.UseRouting();
    app.UseSession();
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