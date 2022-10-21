using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Requests.Querries;
using ServicesContracts.Courses.Responses;
using SessionGatewayService.Application.Contracts;

namespace SessionGatewayService.Application.Features.Courses.Querries.GetCourses;

public class GetCoursesQuerryHandler : IRequestHandler<GetCoursesQuerry, Result<CoursesVm>>
{
    private readonly ICoursesService _coursesService;

    public GetCoursesQuerryHandler(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    public async Task<Result<CoursesVm>> Handle(GetCoursesQuerry request, CancellationToken cancellationToken)
    {
        var responce = await _coursesService.GetCoursesAsync(request, cancellationToken);
        if (responce.IsSuccess) return Result.Success(responce.Value);
        if (responce.Errors.Count() != 0) return Result.Error(responce.Errors);
        return Result.Invalid(responce.ValidationErrors.ToList());
    }
}
