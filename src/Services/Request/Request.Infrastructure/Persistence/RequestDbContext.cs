using Microsoft.EntityFrameworkCore;
using Request.Domain.Entities;
using Request.Infrastructure.Persistence.DbMap;

namespace Request.Infrastructure.Persistence;

public class RequestDbContext : DbContext

{
    public DbSet<RequestDbModel> Requests { get; set; }

    public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RequestDbMap());
    }

}