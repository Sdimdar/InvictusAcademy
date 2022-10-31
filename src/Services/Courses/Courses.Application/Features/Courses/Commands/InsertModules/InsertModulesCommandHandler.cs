using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.InsertModules;

public class InsertModulesCommandHandler : IRequestHandler<InsertModulesCommand, Result<CourseInfoDbModel>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IValidator<InsertModulesCommand> _validate;

    public InsertModulesCommandHandler(IValidator<InsertModulesCommand> validate,
                                       ICourseInfoRepository courseInfoRepository)
    {
        _validate = validate;
        _courseInfoRepository = courseInfoRepository;
    }

    public async Task<Result<CourseInfoDbModel>> Handle(InsertModulesCommand request,
                                                        CancellationToken cancellationToken)
    {
        var validatorResult = await _validate.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }

        try
        {
            return Result.Success(await _courseInfoRepository.InsertModulesAsync(request.CourseId,
                                                                                 request.ModulesId,
                                                                                 request.StartIndex,
                                                                                 cancellationToken));
        }
        catch (KeyNotFoundException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
