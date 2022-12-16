using Amazon.S3;
using Amazon.S3.Model;
using CloudStorage.Application.Contracts;
using CloudStorage.Domain.Entities;
using CloudStorage.Infrastructure.Persistence;
using CommonRepository;

namespace CloudStorage.Infrastructure.Repositories;

public class CloudStorageRepository : BaseRepository<CloudStorageDbModel, CloudStorageDbContext>, ICloudStorageRepository
{
    public CloudStorageRepository(CloudStorageDbContext dbContext) : base(dbContext) {}
    protected override IQueryable<CloudStorageDbModel> FilterByString(IQueryable<CloudStorageDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.FileName.ToLower().Contains(filterString.ToLower())
                               || v.FileName.ToLower().Contains(filterString.ToLower())
            );
    }

}