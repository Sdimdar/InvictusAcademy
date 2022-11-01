using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Querries;

namespace Courses.Application.Features.Courses.Queries.GetCourseModulesId;

public class GetCourseModuleIdQuerryHandler : IRequestHandler<GetCourseModulesIdQuerry, Result<List<int>>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IValidator<GetCourseModulesIdQuerry> _validator;

    public GetCourseModuleIdQuerryHandler(ICourseInfoRepository courseInfoRepository,
                                          IValidator<GetCourseModulesIdQuerry> validator)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
    }

    public async Task<Result<List<int>>> Handle(GetCourseModulesIdQuerry request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }

        try
        {
            return Result.Success(await _courseInfoRepository.GetModulesId(request.CourseId, cancellationToken));
        }
        catch (KeyNotFoundException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
