using AutoMapper;
using Jitsi.API.Models.DbModels;
using ServicesContracts.Jitsi;
using ServicesContracts.Jitsi.Commands;
using ServicesContracts.Jitsi.Models;

namespace Jitsi.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateStreamingRoomCommand, StreamingRoomDbModel>();
        CreateMap<StreamingRoomDbModel, StreamingRoomVm>();
    }
}