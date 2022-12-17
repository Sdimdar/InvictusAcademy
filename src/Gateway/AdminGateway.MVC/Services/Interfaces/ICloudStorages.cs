using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.CloudStorage.Requests.Commands;
using ServicesContracts.CloudStorage.Requests.Queries;
using ServicesContracts.CloudStorage.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface ICloudStorages : IUseExtendedHttpClient<ICloudStorages>
{
    Task<DefaultResponseObject<string>> Upload (UploadFileCommand request);
    
    Task<DefaultResponseObject<GetAllFilesVM>> GetFilesAsync(int pageNumber, int pageSize, string? filterString);
    Task<DefaultResponseObject<int>> GetFilesCount();
    Task<DefaultResponseObject<List<GetAllFilesVM>>> GetFilterByString(GetFilesByFilterStringQuery request);

}