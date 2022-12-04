using Ardalis.Result;
using Courses.Domain.Entities;
using MediatR;

namespace ServicesContracts.Courses.Requests.Courses.Commands;

public class CoursePointsVm : IRequest<Result<CoursePointsDbModel>>
{
    public string Point { get; set; }
    public string PointImageLink { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
    
}