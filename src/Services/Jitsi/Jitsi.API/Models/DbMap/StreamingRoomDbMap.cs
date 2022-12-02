using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jitsi.API.Models.DbMap;

public class StreamingRoomDbMap : IEntityTypeConfiguration<StreamingRoomDbMap>
{
    public void Configure(EntityTypeBuilder<StreamingRoomDbMap> builder)
    {
        throw new NotImplementedException();
    }
}