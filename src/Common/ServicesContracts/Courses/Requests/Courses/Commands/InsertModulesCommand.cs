using Ardalis.Result;
using Courses.Domain.Entities.CourseInfo;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class InsertModulesCommand : IRequest<Result<CourseInfoDbModel>>
{
    public int CourseId { get; set; }
    public IEnumerable<int> ModulesId { get; set; }
    public int StartIndex { get; set; }
}
