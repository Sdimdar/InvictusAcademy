using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Infrastructure.Persistance.DbMap;

public class CoursePointsDbMap: IEntityTypeConfiguration<CoursePointsDbModel>
{
    public void Configure(EntityTypeBuilder<CoursePointsDbModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.Point).HasColumnType("VARCHAR(500)").IsRequired();
        builder.Property(p => p.PointImageLink).HasColumnType("VARCHAR(100)").IsRequired();
        builder.HasOne<CourseDbModel>(p => p.Course)
            .WithMany(p => p.CoursePoints)
            .HasForeignKey(p => p.CourseId);
    }
}