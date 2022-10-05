using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistance.DbMap;

public class RequestDbMap : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.Property(r => r.UserName).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(r => r.PhoneNumber).HasColumnType("VARCHAR(13)").IsRequired();
        builder.Property(r => r.ManagerComment).HasColumnType("VARCHAR(13)");
        builder.Property(r => r.CreatedDate).HasColumnType("TIMESTAMP").IsRequired();
        builder.Property(r => r.WasCalled).HasColumnType("BOOLEAN");
    }
}