using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Delete;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    private readonly IValidator<DeleteCourseCommand> _validator;
    private readonly ICourseRepository _courseRepository;

    public DeleteCourseCommandHandler(IValidator<DeleteCourseCommand> validator,
                                      ICourseRepository courseRepository)
    {
        _validator = validator;
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }
        var entity = await _courseRepository.GetByIdAsync(request.Id);
        if (entity is null) return Result.Error($"Course with Id: {request.Id} not found");
        await _courseRepository.DeleteAsync(entity);
        return Result.Success(); 
    }
}
