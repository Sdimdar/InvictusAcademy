using Identity.API;
using Identity.Application;
using Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddMvc();
services.AddEndpointsApiExplorer();
services.AddControllers().AddNewtonsoftJson();
services.AddSwaggerConfiguration();
services.SetJwtTokenServices(builder.Configuration);

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
app.UseCors("CorsPolicy");
app.MapControllers();
app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["jwt"];
    if (!string.IsNullOrEmpty(token))
        context.Request.Headers.Add("Authorization", "Bearer " + token);

    await next();
});
app.UseAuthentication();
app.UseAuthorization();

app.Run();
