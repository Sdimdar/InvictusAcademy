using DataTransferLib.Models;
using ExtendedHttpClient;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class StreamingRoomService : IStreamingRoomService
{
    public ExtendedHttpClient<IStreamingRoomService> ExtendedHttpClient { get; set; }
    
    public StreamingRoomService(ExtendedHttpClient<IStreamingRoomService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }
    
    public async Task<DefaultResponseObject<AllStreamingRoomsVm>> GetAll(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<AllStreamingRoomsVm>>(
            $"/StreamingRooms/GetAll?pageNumber={request.PageNumber}&pageSize={request.PageSize}", cancellationToken);   
    }

    public async Task<DefaultResponseObject<int>> GetCount(CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<int>>($"/StreamingRooms/GetCount", cancellationToken);
    }

    public async Task<DefaultResponseObject<StreamingRoomVm>> GetByAddress(GetByAddressQuery request, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<StreamingRoomVm>>($"/StreamingRooms/GetByAddress?Address={request.Address}", cancellationToken);
    }
}