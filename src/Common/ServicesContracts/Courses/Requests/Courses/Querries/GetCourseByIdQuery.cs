using Ardalis.Result;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCourseByIdQuery : IRequest<Result<CourseByIdVm>>
{
    public int Id { get; set; }
}
