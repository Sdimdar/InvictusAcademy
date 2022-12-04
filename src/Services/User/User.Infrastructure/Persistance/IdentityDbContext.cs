using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Infrastructure.Persistance.DbMap;

namespace User.Infrastructure.Persistance;

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
