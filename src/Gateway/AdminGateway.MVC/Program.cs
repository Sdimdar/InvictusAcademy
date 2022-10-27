using AdminGateway.MVC.DependencyInjection;
using AdminGateway.MVC.Models;
using AdminGateway.MVC.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


string connection = builder.Configuration.GetConnectionString("AdminConnection");
services.AddDbContext<AdminDbContext>(options => options.UseNpgsql(connection));
services.AddIdentity<AdminUser, IdentityRole>().AddEntityFrameworkStores<AdminDbContext>();
// Add services to the container.
services.AddControllersWithViews();
//swagger
services.AddSwaggerConfiguration();

//custom services
services.AddCustomServices();

//mapper
services.SetAutomapperProfiles();


services.AddHttpClients(configuration);

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");

app.Run();