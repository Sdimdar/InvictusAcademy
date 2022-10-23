using UserGateway.API;
using UserGateway.Application;
using UserGateway.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerConfiguration();
services.ConfigureSessionServices();
services.SetAutomapperProfiles();

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

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.UseRouting();
app.UseSession();
app.MapControllers();


app.Run();
