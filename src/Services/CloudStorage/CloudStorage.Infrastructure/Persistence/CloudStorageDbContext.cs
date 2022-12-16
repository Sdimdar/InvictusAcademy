using CloudStorage.Domain.Entities;
using CloudStorage.Infrastructure.Persistence.DbMap;
using Microsoft.EntityFrameworkCore;

namespace CloudStorage.Infrastructure.Persistence;

public class CloudStorageDbContext : DbContext
{
    public DbSet<CloudStorageDbModel> CloudStorageFiles { get; set; }
    public CloudStorageDbContext(DbContextOptions<CloudStorageDbContext> options) : base(options) 
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new CloudStorageDbMap());
    }
}