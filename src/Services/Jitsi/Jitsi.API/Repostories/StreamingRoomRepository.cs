using System.Linq.Expressions;
using CommonRepository;
using Jitsi.API.Models;
using Jitsi.API.Models.DbModels;
using Jitsi.API.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public override async Task<StreamingRoomDbModel> AddAsync(StreamingRoomDbModel entity)
    {
        entity.CreatedDate = DateTime.Now;
        entity.LastModifiedDate = DateTime.Now;
        Context.Set<StreamingRoomDbModel>().Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    
    public override async Task<List<StreamingRoomDbModel>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null)
    {
        return await FilterByString(Context.Set<StreamingRoomDbModel>(), filterString)
            .Where(p=>p.IsOpened==true)
            .OrderByDescending(e => e.LastModifiedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
    }
}