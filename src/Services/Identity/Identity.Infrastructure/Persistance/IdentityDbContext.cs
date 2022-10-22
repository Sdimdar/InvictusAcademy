using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance.DbMap;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance;

public class IdentityDbContext : DbContext
{
    
    public DbSet<UserDbModel> Users { get; set; }
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) 
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserDbMap());
    }
}
