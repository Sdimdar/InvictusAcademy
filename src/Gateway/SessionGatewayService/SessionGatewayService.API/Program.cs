using SessionGatewayService.API;
using SessionGatewayService.Application;
using SessionGatewayService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerConfiguration();
services.ConfigureSessionServices();

// Add API services
services.AddInfrastructureServices(builder.Configuration);
services.AddApplicationServices();

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
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.UseRouting();
app.UseSession();
app.MapControllers();


app.Run();
