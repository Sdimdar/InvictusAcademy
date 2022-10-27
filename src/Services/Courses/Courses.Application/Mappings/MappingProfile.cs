using AutoMapper;
using Courses.Domain.Entities;
using ServicesContracts.Courses.Requests.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateCourseCommand, CourseDbModel>();
		CreateMap<EditCourseCommand, CourseDbModel>();
		CreateMap<CourseDbModel, CourseVm>();
	}
}
