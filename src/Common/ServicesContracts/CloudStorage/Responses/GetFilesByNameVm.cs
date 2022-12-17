using CloudStorage.Domain.Entities;

namespace ServicesContracts.CloudStorage.Responses;

public class GetFilesByNameVm
{
    public List<CloudStorageDbModel> Files { get; set; }
}