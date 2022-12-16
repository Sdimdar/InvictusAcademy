using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Querries;

namespace Courses.Application.Features.Courses.Queries.GetCourseModulesId;

public class GetCourseModulesIdQuerryHandler : IRequestHandler<GetCourseModulesIdQuerry, Result<UniqueList<int>>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IValidator<GetCourseModulesIdQuerry> _validator;
    private readonly ILogger<GetCourseModulesIdQuerryHandler> _logger;

    public GetCourseModulesIdQuerryHandler(ICourseInfoRepository courseInfoRepository,
                                          IValidator<GetCourseModulesIdQuerry> validator,
                                          ILogger<GetCourseModulesIdQuerryHandler> logger)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<UniqueList<int>>> Handle(GetCourseModulesIdQuerry request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }

        try
        {
            var courseInfo = await _courseInfoRepository.GetAsync(request.CourseId, cancellationToken);
            if (courseInfo is null)
            {
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.CourseId} not found");
            }
            return Result.Success(courseInfo.ModulesId);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError($"{BussinesErrors.KeyNotFoundException}: {ex.Message}");
            return Result.Error($"{BussinesErrors.KeyNotFoundException}: {ex.Message}");
        }
    }
}
