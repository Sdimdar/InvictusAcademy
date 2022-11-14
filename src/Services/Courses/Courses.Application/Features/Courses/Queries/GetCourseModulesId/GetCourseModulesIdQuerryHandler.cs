using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;

namespace Courses.Application.Features.Courses.Queries.GetCourseModulesId;

public class GetCourseModulesIdQuerryHandler : IRequestHandler<GetCourseModulesIdQuerry, Result<UniqueList<int>>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IValidator<GetCourseModulesIdQuerry> _validator;

    public GetCourseModulesIdQuerryHandler(ICourseInfoRepository courseInfoRepository,
                                          IValidator<GetCourseModulesIdQuerry> validator)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
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
            if (courseInfo is null) return Result.Error($"Course with Id: {request.CourseId} not found");
            return Result.Success(courseInfo.ModulesId);
        }
        catch (KeyNotFoundException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
