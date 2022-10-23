using User.API;
using User.Application;
using User.Infrastructure;

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }