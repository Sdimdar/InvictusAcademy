using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.CloudStorage.Requests.Commands;
using ServicesContracts.CloudStorage.Requests.Queries;
using ServicesContracts.CloudStorage.Responses;
using ServicesContracts.Identity.Responses;

namespace AdminGateway.MVC.Services;

public class CloudStorage : ICloudStorages
{
    public ExtendedHttpClient<ICloudStorages> ExtendedHttpClient { get; set; }

    public CloudStorage(ExtendedHttpClient<ICloudStorages> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }
    
    
    public async Task<DefaultResponseObject<string>> Upload(UploadFileCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<UploadFileCommand, DefaultResponseObject<string>>(request, $"/CloudStorage/UploadFile");
    }

    public async Task<DefaultResponseObject<GetAllFilesVM>> GetFilesAsync(int pageNumber, int pageSize)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<GetAllFilesVM>>(
            $"/CloudStorage/GetAllFiles?pageNumber={pageNumber}&pageSize={pageSize}");
    }

    public async Task<DefaultResponseObject<int>> GetFilesCount()
    {
        return await ExtendedHttpClient
            .GetAndReturnResponseAsync<DefaultResponseObject<int>>("/CloudStorage/GetFilesCount");
    }
    
    public async Task<DefaultResponseObject<List<GetAllFilesVM>>> GetFilterByString(GetFilesByFilterStringQuery filterString)
    {
        return await ExtendedHttpClient
            .GetAndReturnResponseAsync<GetFilesByFilterStringQuery, 
                DefaultResponseObject<List<GetAllFilesVM>>>(filterString,$"/CloudStorage/GetFilterByString?filteredString={filterString}");
    }
}