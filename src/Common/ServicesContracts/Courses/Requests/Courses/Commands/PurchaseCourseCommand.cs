using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class PurchaseCourseCommand : IRequest<Result<bool>>
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
}