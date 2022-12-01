using CommonRepository;
using Jitsi.API.Models;
using Jitsi.API.Models.DbModels;
using Jitsi.API.Repostories.Interfaces;

namespace Jitsi.API.Repostories;

public class StreamingRoomRepository : BaseRepository<StreamingRoomDbModel, StreamingRoomDbContext>, IStreamingRoomRepository
{
    public StreamingRoomRepository(StreamingRoomDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<StreamingRoomDbModel> FilterByString(IQueryable<StreamingRoomDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.Name.ToLower().Contains(filterString.ToLower())
            );
    }
}