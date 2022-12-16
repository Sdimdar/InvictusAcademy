using AdminGateway.MVC.Services.Interfaces;
using CommonStructures;
using Courses.Domain.Entities.CourseInfo;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ExtendedHttpClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;

namespace AdminGateway.MVC.Services;

public class StreamingRoomService : IStreamingRoomService
{
    public ExtendedHttpClient<IStreamingRoomService> ExtendedHttpClient { get; set; }
    public StreamingRoomService(ExtendedHttpClient<IStreamingRoomService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }


    public async Task<DefaultResponseObject<string>> Create(CreateStreamingRoomCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CreateStreamingRoomCommand, DefaultResponseObject<string>>(request, $"/StreamingRooms/Create");

    }

    public async Task<DefaultResponseObject<string>> OpenOrCloseRoom(string address)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<string, DefaultResponseObject<string>>(address, $"/StreamingRooms/OpenOrCloseRoom");

    }

    public async Task<DefaultResponseObject<AllStreamingRoomsVm>> GetAll(GetAllRoomsQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<AllStreamingRoomsVm>>(
            $"/StreamingRooms/GetAll?pageNumber={request.PageNumber}&pageSize={request.PageSize}");    
    }

    public async Task<DefaultResponseObject<int>> GetCount()
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<int>>($"/StreamingRooms/GetCount");
    }

    public async Task<DefaultResponseObject<StreamingRoomVm>> GetByAddress(GetByAddressQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<StreamingRoomVm>>($"/StreamingRooms/GetByAddress?Address={request.Address}");
    }
}