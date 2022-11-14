using AdminGateway.MVC.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminGateway.MVC.Models;

public class AdminDbContext : IdentityDbContext<AdminUser>
{
    public DbSet<AdminUser> Users { get; set; }
    public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
    {
        Database.Migrate();
    }

}