using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Commands;

public class CreateCourseCommand : IRequest<Result<string>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? VideoLink { get; set; }
    public decimal Cost { get; set; }
    public bool IsActive { get; set; }
}
