using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IStreamingRoomService : IUseExtendedHttpClient<IStreamingRoomService>
{
    Task<DefaultResponseObject<string>> Create(CreateStreamingRoomCommand request);
    Task<DefaultResponseObject<string>> OpenOrCloseRoom(string address);
    Task<DefaultResponseObject<AllStreamingRoomsVm>> GetAll(GetAllRoomsQuery request);
    Task<DefaultResponseObject<int>> GetCount();
    Task<DefaultResponseObject<StreamingRoomVm>> GetByAddress(GetByAddressQuery request);
}