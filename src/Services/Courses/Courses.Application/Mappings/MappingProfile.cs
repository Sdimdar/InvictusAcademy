using AutoMapper;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateCourseCommand, CourseDbModel>();
		CreateMap<EditCourseCommand, CourseDbModel>();
		CreateMap<CreateModuleCommand, ModuleInfoDbModel>();
		CreateMap<UpdateModuleCommand, ModuleInfoDbModel>();
	}
}
