using AutoMapper;
using Courses.Domain.Entities;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateCourseCommand, CourseDbModel>();
		CreateMap<EditCourseCommand, CourseDbModel>();
	}
}
