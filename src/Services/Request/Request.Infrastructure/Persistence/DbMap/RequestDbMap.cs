using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Request.Domain.Entities;

namespace Request.Infrastructure.Persistence.DbMap;

public class RequestDbMap : IEntityTypeConfiguration<RequestDbModel>
{
    public void Configure(EntityTypeBuilder<RequestDbModel> builder)
    {
        builder.Property(r => r.UserName).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(r => r.PhoneNumber).HasColumnType("VARCHAR(13)").IsRequired();
        builder.HasIndex(p => p.PhoneNumber);
        builder.Property(r => r.ManagerComment).HasColumnType("VARCHAR(100)");
        builder.Property(r => r.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(r => r.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(r => r.WasCalled).HasColumnType("BOOLEAN");
    }
}