using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Queries.GetFullModulesInfoByCourseId;

public class GetFullModulesInfoByCourseIdHandler:IRequestHandler<GetFullByCourseIdGatewayQuery, Result<List<ModuleInfoVm>>>
{
    private readonly ICoursesService _coursesService;
    private readonly IUserService _userService;

    public GetFullModulesInfoByCourseIdHandler(ICoursesService coursesService, IUserService userService)
    {
        _coursesService = coursesService;
        _userService = userService;
    }

    public async Task<Result<List<ModuleInfoVm>>> Handle(GetFullByCourseIdGatewayQuery request, CancellationToken cancellationToken)
    {
        var userResponse = await _userService.GetUserAsync(request.UserEmail, cancellationToken);
        if (!userResponse.IsSuccess)
            return Result.Error(userResponse.Errors[0]);
        
        var innerRequest = new GetFullByCourseIdQuery
        {
            CourseId = request.CourseId,
            UserId = userResponse.Value.Id
        };
        
        var response = await _coursesService.GetModulesInfoByCourseId(innerRequest, cancellationToken);
        if (response.IsSuccess) return Result.Success(response.Value);
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}