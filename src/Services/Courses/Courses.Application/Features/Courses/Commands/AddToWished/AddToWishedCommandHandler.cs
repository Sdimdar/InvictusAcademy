using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.AddToWished;

public class AddToWishedCommandHandler :IRequestHandler<AddToWishedCourseCommand, Result>
{ 
    private readonly IValidator<AddToWishedCourseCommand> _validator;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseWishedRepository _wishedCourseRepository;
    private readonly ILogger<PurchaseCourseCommand> _logger;

    public AddToWishedCommandHandler(IValidator<AddToWishedCourseCommand> validator, ICourseRepository courseRepository, 
        ILogger<PurchaseCourseCommand> logger, ICourseWishedRepository wishedCourseRepository)
    {
        _validator = validator;
        _courseRepository = courseRepository;
        _logger = logger;
        _wishedCourseRepository = wishedCourseRepository;
    }
    
    public async Task<Result> Handle(AddToWishedCourseCommand request, CancellationToken cancellationToken)
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

        var previousWishes = _wishedCourseRepository
            .GetFirstOrDefaultAsync(c => c.CourseId == request.CourseId && c.UserId == request.UserId).Result;

        if (previousWishes is null)
        {
            try
            {
                CourseWishedDbModel courseWishedDbModel = new()
                {
                    UserId = request.UserId,
                    CourseId = request.CourseId,
                    Course = course,
                    CreatedDate = DateTime.Now
                };
            
                await _wishedCourseRepository.AddAsync(courseWishedDbModel);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
        
        return Result.Error("Курс уже добавлен в избранное");
    }
}