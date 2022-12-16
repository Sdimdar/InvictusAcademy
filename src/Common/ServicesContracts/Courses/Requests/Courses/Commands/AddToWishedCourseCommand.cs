using Ardalis.Result;
using Courses.Domain.Entities;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class AddToWishedCourseCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
}