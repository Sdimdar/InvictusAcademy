using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Courses.Queries.GetShortCourseInfo;

public class GetShortModulesInfoByCourseIdHandler : IRequestHandler<GetShortCourseInfoQuery, Result<List<ShortModuleInfoVm>>>
{
    private readonly ICoursesService _coursesService;

    public GetShortModulesInfoByCourseIdHandler(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    public async Task<Result<List<ShortModuleInfoVm>>> Handle([FromQuery] GetShortCourseInfoQuery request, CancellationToken cancellationToken)
    {
        var response = await _coursesService.GetShortModulesInfoByCourseId(request, cancellationToken);
        if (response.IsSuccess) return Result.Success(response.Value);
        if (response.Errors.Count() != 0) return Result.Error(response.Errors);
        return Result.Invalid(response.ValidationErrors.ToList());
    }
}