using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;
using UserGateway.Application.Features.Courses.Queries.GetCourses;

namespace UserGateway.Application.Features.Courses.Queries.GetCourses;

public class GetCoursesQuerryHandler : IRequestHandler<GetGatewayCoursesQuery, Result<CoursesVm>>
{
    private readonly ICoursesService _coursesService;
    private readonly IUserService _userService;

    public GetCoursesQuerryHandler(ICoursesService coursesService, IUserService userService)
    {
        _coursesService = coursesService;
        _userService = userService;
    }

    public async Task<Result<CoursesVm>> Handle(GetGatewayCoursesQuery request, CancellationToken cancellationToken)
    {
        var userResponse = await _userService.GetUserAsync(request.Email, cancellationToken);
        if (!userResponse.IsSuccess)
            return Result.Error(userResponse.Errors[0]);
        var innerRequest = new GetCoursesQuery
        {
            UserId = userResponse.Value.Id,
            Type = request.Type
        };
        var response = await _coursesService.GetCoursesAsync(innerRequest, cancellationToken);
        if (response.IsSuccess) return Result.Success(response.Value);
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}
