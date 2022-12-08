using Jitsi.API.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jitsi.API.Models.DbMap;

public class StreamingRoomDbMap : IEntityTypeConfiguration<StreamingRoomDbModel>
{
    public void Configure(EntityTypeBuilder<StreamingRoomDbModel> builder)
    {
        builder.Property(p => p.CreatedDate).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()").IsRequired();
        builder.Property(p => p.LastModifiedDate).HasColumnType("TIMESTAMP");
        builder.Property(p => p.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.Property(p => p.Address).HasColumnType("VARCHAR(100)").IsRequired();
    }
}