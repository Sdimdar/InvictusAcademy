using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.RemoveFromWished;

public class RemoveFromWishedCommandHandler :IRequestHandler<RemoveFromWishedCommand, Result>
{
    private readonly IValidator<RemoveFromWishedCommand> _validator;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseWishedRepository _wishedCourseRepository;
    private readonly ILogger<PurchaseCourseCommand> _logger;

    public RemoveFromWishedCommandHandler(IValidator<RemoveFromWishedCommand> validator, 
        ICourseRepository courseRepository, ICourseWishedRepository wishedCourseRepository, ILogger<PurchaseCourseCommand> logger)
    {
        _validator = validator;
        _courseRepository = courseRepository;
        _wishedCourseRepository = wishedCourseRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(RemoveFromWishedCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogInformation($"UserId - {request.UserId}, CourseId - {request.CourseId}. Is invalid");
            return Result.Invalid(validationResult.AsErrors());
        }

        var course = await _courseRepository.GetCourseById(request.CourseId);
        if (course is null)
        {
            _logger.LogInformation($"Course with Id: \'{request.CourseId}\' not founded");
            return Result.Error("Course is not found by ID");
        }

        var wishedCourse = _wishedCourseRepository
            .GetFirstOrDefaultAsync(c => c.CourseId == request.CourseId && c.UserId == request.UserId).Result;

        if (wishedCourse != null)
        {
            try
            {
                await _wishedCourseRepository.DeleteAsync(wishedCourse);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
        
        return Result.Error("Такого курса нет в избранных");

    }
}