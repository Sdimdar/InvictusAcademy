using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetModulesByFilterStringQuery : IRequest<Result<List<ModuleInfoDbModel>?>>
{
    public string FilterString { get; set; }
}
