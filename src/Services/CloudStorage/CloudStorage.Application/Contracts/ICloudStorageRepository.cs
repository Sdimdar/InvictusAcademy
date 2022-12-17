using CloudStorage.Domain.Entities;
using CommonRepository.Abstractions;
using DataTransferLib.Models;
using ServicesContracts.CloudStorage.Requests.Queries;
using ServicesContracts.CloudStorage.Responses;

namespace CloudStorage.Application.Contracts;

public interface ICloudStorageRepository : IBaseRepository<CloudStorageDbModel>
{
    Task<bool> GetFilesByName(string fileName);
    Task<List<CloudStorageDbModel>> GetFilerByFilterString(string filterString, CancellationToken cancellationToken);
}