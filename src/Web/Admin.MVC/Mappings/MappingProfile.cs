using Admin.MVC.ViewModels;
using AutoMapper;
using ServicesContracts.Identity.Responses;

namespace Admin.MVC.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserVm, RegisteredUserVM>();
    }
}