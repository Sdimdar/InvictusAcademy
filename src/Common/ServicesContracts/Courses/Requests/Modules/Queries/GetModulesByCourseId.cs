using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetModulesByCourseId : IRequest<Result<List<ModuleInfoDbModel>?>>
{
    public int CourseId { get; set; }
}