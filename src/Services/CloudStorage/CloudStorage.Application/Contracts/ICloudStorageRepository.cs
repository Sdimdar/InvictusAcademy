using CloudStorage.Domain.Entities;
using CommonRepository.Abstractions;

namespace CloudStorage.Application.Contracts;

public interface ICloudStorageRepository : IBaseRepository<CloudStorageDbModel>
{
}