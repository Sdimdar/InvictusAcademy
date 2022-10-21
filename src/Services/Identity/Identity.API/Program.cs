using Identity.API;
using Identity.Application;
using Identity.Infrastructure;
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddMvc();
services.AddEndpointsApiExplorer();
services.AddControllers().AddNewtonsoftJson();
services.AddSwaggerConfiguration();

// Add API services
services.AddInfrastructureServices(builder.Configuration);
services.AddApplicationServices();

// Add Automapper maps
services.SetAutomapperProfiles();


var app = builder.Build();

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
serviceScope.ServiceProvider.GetRequiredService<IdentityDbContext>().Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
