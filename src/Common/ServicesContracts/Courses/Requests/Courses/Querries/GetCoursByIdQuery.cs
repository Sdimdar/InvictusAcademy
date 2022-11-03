using Ardalis.Result;
using Courses.Domain.Entities;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCoursByIdQuery : IRequest<Result<CourseDbModel>>
{
    public int Id { get; set; }
}
