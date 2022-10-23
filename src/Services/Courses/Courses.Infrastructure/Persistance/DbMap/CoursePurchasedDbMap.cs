using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace Courses.Infrastructure.Persistance.DbMap;

public class CoursePurchasedDbMap : IEntityTypeConfiguration<CoursePurchasedDbModel>
{
    public void Configure(EntityTypeBuilder<CoursePurchasedDbModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.CourseResultId).HasColumnType("NUMERIC(7,0)").IsRequired();
        builder.Property(p => p.IsCompleted).HasColumnType("BOOLEAN").HasDefaultValue(false).IsRequired();
        builder.Property(p => p.UserId).HasColumnType("NUMERIC(7,0)").IsRequired();
        builder.HasOne<CourseDbModel>(p => p.Course)
               .WithMany(p => p.CoursePurchased)
               .HasForeignKey(p => p.CourseId);
    }
}
