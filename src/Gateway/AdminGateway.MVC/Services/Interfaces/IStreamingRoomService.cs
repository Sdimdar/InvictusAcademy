using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IStreamingRoomService : IUseExtendedHttpClient<IStreamingRoomService>
{
    Task<ActionResult<DefaultResponseObject<string>>> Create(CreateStreamingRoomCommand request);
    Task<ActionResult<DefaultResponseObject<string>>> OpenOrCloseRoom(string address);
    Task<ActionResult<DefaultResponseObject<AllStreamingRoomsVm>>> GetAll(GetAllRoomsQuery request);
    Task<ActionResult<DefaultResponseObject<int>>> GetCount();
    Task<ActionResult<DefaultResponseObject<StreamingRoomVm>>> GetByAddress(GetByAddressQuery request);
}