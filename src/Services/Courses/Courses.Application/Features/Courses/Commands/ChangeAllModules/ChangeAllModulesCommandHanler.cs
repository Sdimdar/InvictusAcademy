using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.ChangeAllModules;

public class ChangeAllModulesCommandHanler : IRequestHandler<ChangeAllModulesCommand, Result<CourseInfoDbModel>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IValidator<ChangeAllModulesCommand> _validator;

    public ChangeAllModulesCommandHanler(ICourseInfoRepository courseInfoRepository,
                                         IValidator<ChangeAllModulesCommand> validator)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
    }

    public async Task<Result<CourseInfoDbModel>> Handle(ChangeAllModulesCommand request,
                                                        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        try
        {
            return Result.Success(await _courseInfoRepository.ChangeAllModulesAsync(request.CourseId,
                                                                                    request.ModulesId,
                                                                                    cancellationToken));
        }
        catch (KeyNotFoundException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
