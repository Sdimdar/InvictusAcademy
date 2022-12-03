using AutoMapper;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using ServicesContracts.Courses.Responses;

namespace Courses.API.Mappings;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<ModuleInfoDbModel, ModuleInfoVm>();
        CreateMap<ModuleInfoDbModel, ShortModuleInfoVm>();
        CreateMap<CourseDbModel, CourseVm>();
        CreateMap<CourseDbModel, CourseByIdVm>();
        CreateMap<CourseInfoDbModel, CourseInfoVm>()
            .ForMember(p => p.ModulesId,
                       opt => opt.MapFrom(src => src.ModulesString == "" ? new List<int>()
                                                                         : src.ModulesString.Split(',', StringSplitOptions.None)
                                                                                            .AsParallel()
                                                                                            .Select(e => int.Parse(e))
                                                                                            .ToList()
                                                                                            ));
    }
}
