using Ardalis.Result;
using Courses.Domain.Entities;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetCourseDataQuery : IRequest<Result<PurchasedCourseInfoVm>>
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
}
