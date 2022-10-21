using Admin.MVC.Mappings;
using Admin.MVC.Models;
using Admin.MVC.Models.DbModels;
using Admin.MVC.Services;
using Admin.MVC.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


string connection = builder.Configuration.GetConnectionString("AdminConnetion");
services.AddDbContext<AdminDbContext>(options => options.UseNpgsql(connection));
services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AdminDbContext>();
// Add services to the container.
services.AddControllersWithViews();

services.AddTransient<IAdminCreate, CreateAdmin>();
services.AddHttpClient<IGetUsers, GetUsers>(c => c.BaseAddress = new Uri(configuration["ApiSettings:IdentityUrl"]));
services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
}).CreateMapper());

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var options = scope.ServiceProvider;
    try
    {
        var userManager = options.GetRequiredService<UserManager<User>>();
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accounts}/{action=Login}/{id?}");

app.Run();