using AdminGateway.MVC;
using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using GlobalExceptionHandler.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();
services.AddExceptionHandlers();

//swagger
services.AddSwaggerConfiguration();

//custom services
services.AddCustomServices();
services.AddDbServices(builder.Configuration);
services.AddHttpClients(builder.Configuration);

//mapper
services.SetAutomapperProfiles();

// Configure CORS Policy and Cookie
services.SetCorsPolicy();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var options = scope.ServiceProvider;
    try
    {
        var userManager = options.GetRequiredService<UserManager<AdminUser>>();
        var rolesManager = options.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);
    }
    catch (Exception e)
    {
        var logger = options.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();