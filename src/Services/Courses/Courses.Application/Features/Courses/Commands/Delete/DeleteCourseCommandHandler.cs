using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CommonStructures;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Delete;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Result>
{
    private readonly IValidator<DeleteCourseCommand> _validator;
    private readonly ICourseRepository _courseRepository;
    private readonly ILogger<DeleteCourseCommandHandler> _logger;

    public DeleteCourseCommandHandler(IValidator<DeleteCourseCommand> validator,
                                      ICourseRepository courseRepository, ILogger<DeleteCourseCommandHandler> logger)
    {
        _validator = validator;
        _courseRepository = courseRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }
        var entity = await _courseRepository.GetByIdAsync(request.Id);
        if (entity is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.Id} not found");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course with Id: {request.Id} not found");
        }
        await _courseRepository.DeleteAsync(entity);
        return Result.Success(); 
    }
}
