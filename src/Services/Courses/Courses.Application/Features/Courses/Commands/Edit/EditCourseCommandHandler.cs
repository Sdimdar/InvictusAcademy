using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Edit;

public class EditCourseCommandHandler : IRequestHandler<EditCourseCommand,Result<string>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IValidator<EditCourseCommand> _validator;


    public EditCourseCommandHandler(ICourseRepository courseRepository, IValidator<EditCourseCommand> validator)
    {
        _courseRepository = courseRepository;
        _validator = validator;
    }

    public async Task<Result<string>> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if(course is null)
                return Result.Error("Course not found in database");
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                return Result.Invalid(validationResult.AsErrors());
           
            course.Name = request.Name;
            course.Description = request.Description;
            course.VideoLink = request.VideoLink;
            course.Cost = request.Cost;
            course.IsActive = request.IsActive;
            await _courseRepository.UpdateAsync(course);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}