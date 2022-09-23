using Microsoft.EntityFrameworkCore;

namespace InvictusAcademyApp.Infrastructures.Databases;

public class InvictusDbContext : DbContext
{
    
    public InvictusDbContext(DbContextOptions<InvictusDbContext> options) : base(options)
    {
        
    }
}