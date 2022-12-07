using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class RemoveFromWishedCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
}