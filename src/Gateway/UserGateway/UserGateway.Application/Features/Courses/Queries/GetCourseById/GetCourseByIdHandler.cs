using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Queries.GetCourseById;

public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, Result<CourseByIdVm>>
{
    private readonly ICoursesService _coursesService;
    private readonly IUserService _userService;

    public GetCourseByIdHandler(ICoursesService coursesService, IUserService userService)
    {
        _coursesService = coursesService;
        _userService = userService;
    }
    
    public async Task<Result<CourseByIdVm>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _coursesService.GetCourseById(request, cancellationToken);
        if (response.IsSuccess) return Result.Success(response.Value);
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}