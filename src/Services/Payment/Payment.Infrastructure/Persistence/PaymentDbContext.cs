using Microsoft.EntityFrameworkCore;
using Payment.Infrastructure.Persistence.DbMaps;
using Payment.Infrastructure.Persistence.Models;

namespace Payment.Infrastructure.Persistence;

public class PaymentDbContext : DbContext
{
    public DbSet<PaymentRequestDbModel> PaymentRequests { get; set; }

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PaymentRequestDbMap());
    }
}