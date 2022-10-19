using Admin.MVC.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Admin.MVC.Models;

public class AdminDbContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public AdminDbContext(DbContextOptions<AdminDbContext> options): base(options)
    {
        
    }
    
}