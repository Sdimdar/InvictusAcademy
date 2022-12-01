using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Request.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IStreamingRoomsService:IUseExtendedHttpClient<IStreamingRoomsService>
{
    public Task<DefaultResponseObject<string>> Create(CreateStreamingRoomCommand createStreamingRoomCommand);
    public Task<DefaultResponseObject<string>> OpenOrCloseRoom();
    public Task<DefaultResponseObject<AllStreamingRoomsVm>> GetAllAsync(int pageNumber, int pageSize);
}