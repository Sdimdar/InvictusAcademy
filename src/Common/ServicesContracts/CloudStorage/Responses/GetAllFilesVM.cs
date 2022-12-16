using CloudStorage.Domain.Entities;

namespace ServicesContracts.CloudStorage.Responses;

public class GetAllFilesVM
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<CloudStorageDbModel> Files { get; set; }
    public int RequestsCount { get; set; }
}