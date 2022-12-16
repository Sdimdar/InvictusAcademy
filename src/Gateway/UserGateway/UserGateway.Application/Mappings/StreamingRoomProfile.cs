using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Models;

namespace UserGateway.Application.Mappings;

public class StreamingRoomProfile : Profile
{
    public StreamingRoomProfile()
    {
        // CreateMap(typeof(Task<>), typeof(Result<>));
        CreateMap(typeof(DefaultResponseObject<>), typeof(int));
        CreateMap(typeof(DefaultResponseObject<>), typeof(AllStreamingRoomsVm));
        CreateMap(typeof(DefaultResponseObject<>), typeof(StreamingRoomVm));
    }
}