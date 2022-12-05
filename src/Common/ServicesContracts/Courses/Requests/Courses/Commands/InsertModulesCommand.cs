using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class InsertModulesCommand : IRequest<Result<CourseInfoVm>>
{
    public int CourseId { get; set; }
    public List<int> ModulesId { get; set; }
    public int StartIndex { get; set; }
}
