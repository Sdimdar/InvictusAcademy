using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;

namespace AdminGateway.MVC.Services;

public class StreamingRoomsService : IStreamingRoomsService
{
    public ExtendedHttpClient<IStreamingRoomsService> ExtendedHttpClient { get; set; }
    public StreamingRoomsService(ExtendedHttpClient<IStreamingRoomsService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }
    
    public async Task<DefaultResponseObject<string>> Create(CreateStreamingRoomCommand createStreamingRoomCommand)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<string>>("/StreamingRoom/Create");
    }
    
    public async Task<DefaultResponseObject<string>> OpenOrCloseRoom()
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<string>>("/StreamingRoom/OpenOrCloseRoom");
    }

    public async Task<DefaultResponseObject<AllStreamingRoomsVm>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<AllStreamingRoomsVm>>(
            $"/Request/GetAll?pageNumber={pageNumber}&pageSize={pageSize}");
    }
}