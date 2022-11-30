using CommonRepository.Models;

namespace CloudStorage.Domain.Entities;

public class CloudStorageDbModel : BaseRepositoryEntity
{
    public string FileName { get; set; }
}