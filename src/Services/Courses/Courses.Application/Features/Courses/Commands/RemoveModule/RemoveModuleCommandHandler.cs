using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.RemoveModule;

public class RemoveModuleCommandHandler : IRequestHandler<RemoveModuleCommand, Result<CourseInfoDbModel>>
{
    private readonly ICourseInfoRepository _courseInfoRepository;
    private readonly IValidator<RemoveModuleCommand> _validator;

    public RemoveModuleCommandHandler(ICourseInfoRepository courseInfoRepository,
                                      IValidator<RemoveModuleCommand> validator)
    {
        _courseInfoRepository = courseInfoRepository;
        _validator = validator;
    }

    public async Task<Result<CourseInfoDbModel>> Handle(RemoveModuleCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }

        try
        {
            return Result.Success(await _courseInfoRepository.RemoveModuleAsync(request.CourseId, request.ModuleId, cancellationToken));
        }
        catch (KeyNotFoundException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
