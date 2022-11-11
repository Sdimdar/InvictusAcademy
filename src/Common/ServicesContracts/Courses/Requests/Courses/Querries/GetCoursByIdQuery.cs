using Ardalis.Result;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseInfo;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCoursByIdQuery : IRequest<Result<CourseForAdminVm>>
{
    public int Id { get; set; }
}
