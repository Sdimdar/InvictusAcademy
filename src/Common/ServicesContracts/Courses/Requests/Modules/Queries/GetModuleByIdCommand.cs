using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetModuleByIdQuery : IRequest<Result<ModuleInfoDbModel?>>
{
    public int Id { get; set; }
}
