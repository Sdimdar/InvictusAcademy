using Ardalis.Result;
using Courses.Domain.Entities;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class CreateCourseCommand : IRequest<Result<CourseDbModel>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? VideoLink { get; set; }
    public decimal Cost { get; set; }
    public bool IsActive { get; set; }
}
