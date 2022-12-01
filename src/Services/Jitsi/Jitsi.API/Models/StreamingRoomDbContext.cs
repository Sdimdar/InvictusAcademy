using Jitsi.API.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Jitsi.API.Models;

public class StreamingRoomDbContext : DbContext
{
    public DbSet<StreamingRoomDbModel> StreamingRooms { get; set; }

    public StreamingRoomDbContext(DbContextOptions<StreamingRoomDbContext> options) : base(options)
    {
        
    }
}