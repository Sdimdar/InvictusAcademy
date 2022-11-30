using CloudStorage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudStorage.Infrastructure.Persistence.DbMap;

public class CloudStorageDbMap : IEntityTypeConfiguration<CloudStorageDbModel>
{
    public void Configure(EntityTypeBuilder<CloudStorageDbModel> builder)
    {
        builder.Property(r => r.FileName).HasColumnType("VARCHAR(100)").IsRequired();
        
    }
}