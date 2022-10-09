using Request.API;
using Request.Application;
using Request.Infrastructure;

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

// Configure CORS Policy and Cookie
services.SetCorsPolicy();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();