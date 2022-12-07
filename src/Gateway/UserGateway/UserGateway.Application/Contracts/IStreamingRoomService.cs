using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;

namespace UserGateway.Application.Contracts;

public interface IStreamingRoomService : IUseExtendedHttpClient<IStreamingRoomService>
{
    Task<DefaultResponseObject<AllStreamingRoomsVm>> GetAll(GetAllRoomsQuery request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<int>> GetCount(CancellationToken cancellationToken);
    Task<DefaultResponseObject<StreamingRoomVm>> GetByAddress(GetByAddressQuery request, CancellationToken cancellationToken);
}