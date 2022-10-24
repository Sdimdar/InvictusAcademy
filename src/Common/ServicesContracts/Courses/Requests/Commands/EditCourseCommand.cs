using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Commands;

public class EditCourseCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? VideoLink { get; set; }
    public decimal Cost { get; set; }
    public bool IsActive { get; set; }
}