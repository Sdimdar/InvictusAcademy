using Identity.Domain.Entities;
using Identity.Infrastructure.Persistance.DbMap;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance;

public class IdentityDbContext : IdentityDbContext<User>
{
    public DbSet<Request> Requests { get; set; }
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserDbMap());
        builder.ApplyConfiguration(new RequestDbMap());
    }
}
