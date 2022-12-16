using CloudStorage.Domain.Entities;

namespace ServicesContracts.CloudStorage.Responses;

public class NamesListVm
{
    public List<CloudStorageDbModel> Files { get; set; }
}