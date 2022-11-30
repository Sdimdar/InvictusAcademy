using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain.Models;
using Payment.Infrastructure.Persistence.Models;

namespace Payment.Infrastructure.Persistence.DbMaps;

public class PaymentHistoryDbMap:IEntityTypeConfiguration<PaymentHistoryDbModel>
{
    public void Configure(EntityTypeBuilder<PaymentHistoryDbModel> builder)
    {
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("now()").IsRequired();
        builder.Property(p => p.RejectReason).HasColumnType("VARCHAR(150)");
        builder.Property(p => p.ModifyAdminEmail).HasColumnType("VARCHAR(50)");
        builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.PaymentId);
        builder.HasIndex(p => p.CourseId);
    }
}