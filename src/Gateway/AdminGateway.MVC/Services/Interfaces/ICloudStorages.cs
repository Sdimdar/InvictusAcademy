using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.CloudStorage.Requests.Commands;

namespace AdminGateway.MVC.Services.Interfaces;

public interface ICloudStorages : IUseExtendedHttpClient<ICloudStorages>
{
    Task<DefaultResponseObject<string>> Upload (UploadFileCommand request);
}