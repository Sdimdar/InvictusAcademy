using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Infrastructure.Persistence.Models;

namespace Payment.Infrastructure.Persistence.DbMaps;

public class PaymentRequestDbMap  : IEntityTypeConfiguration<PaymentRequestDbModel>
{
    public void Configure(EntityTypeBuilder<PaymentRequestDbModel> builder)
    {
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.RejectReason).HasColumnType("VARCHAR(150)");
        builder.Property(p => p.ModifyAdminEmail).HasColumnType("VARCHAR(50)");
        builder.HasIndex(p => p.UserEmail);
        builder.HasIndex(p => p.CourseId);
    }
}