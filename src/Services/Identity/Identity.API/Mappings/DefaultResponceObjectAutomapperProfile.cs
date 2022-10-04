using Ardalis.Result;
using AutoMapper;
using DataTransferLib;

namespace Identity.API.Mappings;

public class DefaultResponceObjectAutomapperProfile : Profile
{
	public DefaultResponceObjectAutomapperProfile()
	{
        CreateMap<Ardalis.Result.ValidationError, DataTransferLib.ValidationError>();
        CreateMap(typeof(Result<>), typeof(DefaultResponceObject<>));
    }
}
