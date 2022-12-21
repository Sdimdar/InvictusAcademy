using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Infrastructure.Persistance.DbMap;

public class CourseDbMap : IEntityTypeConfiguration<CourseDbModel>
{
    public void Configure(EntityTypeBuilder<CourseDbModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.Description).HasColumnType("VARCHAR(500)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.SecondName).HasColumnType("VARCHAR(100)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.SecondDescription).HasColumnType("VARCHAR(500)").HasDefaultValue("").IsRequired();
        builder.Property(p => p.VideoLink).HasColumnType("VARCHAR(100)");
        builder.Property(p => p.PreviewLink).HasColumnType("VARCHAR(100)");
        builder.Property(p => p.LogoImageLink).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.Cost).HasColumnType("NUMERIC(15,2)").IsRequired();
        builder.Property(p => p.IsActive).HasColumnType("BOOLEAN").HasDefaultValue(false).IsRequired();
        builder.Property(p => p.PassingDayCount).HasColumnType("INTEGER").IsRequired();
        builder.HasMany<CoursePointsDbModel>(p => p.CoursePoints)
            .WithOne(s => s.Course)
            .HasForeignKey(p => p.CourseId);
        builder.HasMany<CoursePurchasedDbModel>(p => p.CoursePurchased)
               .WithOne(s => s.Course)
               .HasForeignKey(p => p.CourseId);
        builder.HasMany<CourseWishedDbModel>(p => p.CourseWished)
               .WithOne(s => s.Course)
               .HasForeignKey(p => p.CourseId);
    }
}
