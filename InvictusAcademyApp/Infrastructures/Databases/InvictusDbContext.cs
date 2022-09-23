using InvictusAcademyApp.Models;
using InvictusAcademyApp.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvictusAcademyApp.Infrastructures.Databases;

public class InvictusDbContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public InvictusDbContext(DbContextOptions<InvictusDbContext> options) : base(options)
    {
        
    }
}