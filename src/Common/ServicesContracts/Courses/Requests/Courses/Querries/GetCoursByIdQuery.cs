using Ardalis.Result;
using Courses.Domain.Entities;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCoursByIdQuery : IRequest<Result<CourseVm>>
{
    public int Id { get; set; }
}
