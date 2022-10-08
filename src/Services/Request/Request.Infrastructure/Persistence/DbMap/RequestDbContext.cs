using Microsoft.EntityFrameworkCore;

namespace Request.Infrastructure.Persistence.DbMap;

public class RequestDbContext:DbContext

{
    public DbSet<Domain.Entities.Request> Requests { get; set; }

    public RequestDbContext(DbContextOptions<RequestDbContext> options):base(options)
    {
        
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new RequestDbMap());
    }
    
}