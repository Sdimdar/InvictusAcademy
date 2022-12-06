using AutoMapper;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCourseCommand, CourseDbModel>();
        CreateMap<EditCourseCommand, CourseDbModel>();
        CreateMap<CreateModuleCommand, ModuleInfoDbModel>();
        CreateMap<UpdateModuleCommand, ModuleInfoDbModel>();
        CreateMap<CourseDbModel, CoursesByIdVm>();
        CreateMap<CoursePointsDbModel, CoursePointsVm>();
        CreateMap<CoursePointsVm, CoursePointsDbModel>();
    }
}
