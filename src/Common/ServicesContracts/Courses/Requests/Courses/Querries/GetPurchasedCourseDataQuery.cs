using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetPurchasedCourseDataQuery : IRequest<Result<PurchasedCourseInfoVm>>
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
}
