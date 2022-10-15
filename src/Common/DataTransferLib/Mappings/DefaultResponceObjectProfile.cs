using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;

namespace DataTransferLib.Mappings;

public class DefaultResponceObjectProfile : Profile
{
    public DefaultResponceObjectProfile()
    {
        CreateMap(typeof(Result<>), typeof(DefaultResponceObject<>));
    }
}
