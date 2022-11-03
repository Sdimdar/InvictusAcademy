using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Modules.Queries;

public class GetAllModulesQuerry : IRequest<Result<List<ModuleInfoDbModel>>>
{
}