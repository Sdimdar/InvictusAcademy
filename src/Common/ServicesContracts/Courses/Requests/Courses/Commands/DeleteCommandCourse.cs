using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class DeleteCourseCommand : IRequest<Result>
{
    public int Id { get; set; }
}
