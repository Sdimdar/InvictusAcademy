using Amazon.S3;
using Amazon.S3.Model;
using CloudStorage.Application.Contracts;
using CloudStorage.Domain.Entities;
using CloudStorage.Infrastructure.Persistence;
using CommonRepository;
using DataTransferLib.Models;
using Microsoft.EntityFrameworkCore;
using ServicesContracts.CloudStorage.Requests.Queries;
using ServicesContracts.CloudStorage.Responses;

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

    public async Task<bool> GetFilesByName(string fileName)
    {
        var query = await Context.CloudStorageFiles
                    .FirstOrDefaultAsync(c => c.FileName == fileName);
                if (query is null) return false;
                return true;
    }

    public async Task<List<CloudStorageDbModel>> GetFilerByFilterString(string filterString, CancellationToken cancellationToken)
    {
        IQueryable<CloudStorageDbModel> query = Context.CloudStorageFiles
            .Where(c => c.FileName.ToLower().Contains(filterString.ToLower()));
        return query.ToList();
    }
}