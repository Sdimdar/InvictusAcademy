using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetShortCourseInfoQuery : IRequest<Result<List<ShortModuleInfoVm>>>
{
    public int CourseId { get; set; }
}