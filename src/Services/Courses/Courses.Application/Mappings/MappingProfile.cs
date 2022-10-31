using AutoMapper;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
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
		CreateMap<CourseInfoDbModel, CourseInfoVm>()
			.ForMember(p => p.ModulesId,
					   opt => opt.MapFrom(src =>
										  src.CourseInfo.ModulesString.Split(',', StringSplitOptions.None)
																	  .AsParallel()
																	  .Select(e => int.Parse(e))
																	  .ToList()
										 )
					   );
	}
}
