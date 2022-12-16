using CommonRepository.Models;

namespace CloudStorage.Domain.Entities;

public class CloudStorageDbModel : BaseRepositoryEntity
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
}