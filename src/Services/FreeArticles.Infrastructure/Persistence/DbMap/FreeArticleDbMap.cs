using FreeArticles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeArticles.Infrastructure.Persistence.DbMap;

public class FreeArticleDbMap : IEntityTypeConfiguration<FreeArticleDbModel>
{
    public void Configure(EntityTypeBuilder<FreeArticleDbModel> builder)
    {
        builder.Property(r => r.Title).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(r => r.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(r => r.LastModifiedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
    }
}