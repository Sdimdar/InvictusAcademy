using GlobalExceptionHandler.Extensions;
using Payment.API;
using Payment.Application;
using Payment.Domain;
using Payment.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddMvc();
services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddSwaggerConfiguration();
services.AddExceptionHandlers();

// Add API services
services.AddInfrastructureServices(builder.Configuration);
services.AddApplicationServices();
services.AddDomainServices();

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