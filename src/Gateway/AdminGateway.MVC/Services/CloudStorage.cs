using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.CloudStorage.Requests.Commands;

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
        return await ExtendedHttpClient.PostAndReturnResponseAsync<UploadFileCommand, DefaultResponseObject<string>>(request, $"/CloudService/UploadFile");
    }
}