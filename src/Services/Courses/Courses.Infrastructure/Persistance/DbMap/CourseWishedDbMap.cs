using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace Courses.Infrastructure.Persistance.DbMap;

public class CourseWishedDbMap : IEntityTypeConfiguration<CourseWishedDbModel>
{
    public void Configure(EntityTypeBuilder<CourseWishedDbModel> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.UserId).HasColumnType("NUMERIC(7,0)").IsRequired();
        builder.HasOne<CourseDbModel>(p => p.Course)
               .WithMany(p => p.CourseWished)
               .HasForeignKey(p => p.CourseId);
    }
}
