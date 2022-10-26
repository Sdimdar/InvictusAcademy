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
		CreateMap<List<CoursePurchasedDbModel>,List<CourseVm>>();
		CreateMap<List<CourseDbModel>,List<CourseVm>>();
		CreateMap<List<CourseWishedDbModel>,List<CourseVm>>();
		CreateMap<List<CourseVm>, CoursesVm>();
	}
}
