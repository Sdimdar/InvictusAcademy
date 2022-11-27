using FreeArticles.Domain.Entities;
using FreeArticles.Infrastructure.Persistence.DbMap;
using Microsoft.EntityFrameworkCore;

namespace FreeArticles.Infrastructure.Persistence;

public class FreeArticleDbContext : DbContext
{
    public DbSet<FreeArticleDbModel> FreeArticles { get; set; }

    public FreeArticleDbContext(DbContextOptions<FreeArticleDbContext> options) : base(options) 
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new FreeArticleDbMap());
    }
}