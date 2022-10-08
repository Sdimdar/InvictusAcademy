using Microsoft.EntityFrameworkCore;

namespace Request.Infrastructure.Persistence.DbMap;

public class AdminDbContext:DbContext

{
    public DbSet<Domain.Entities.Request> Requests { get; set; }

    public AdminDbContext(DbContextOptions<AdminDbContext> options):base(options)
    {
        
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new RequestDbMap());
    }
    
}