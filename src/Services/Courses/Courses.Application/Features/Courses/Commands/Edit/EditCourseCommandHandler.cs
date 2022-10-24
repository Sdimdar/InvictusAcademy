using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.Edit;

public class EditCourseCommandHandler : IRequestHandler<EditCourseCommand,Result<string>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<EditCourseCommand> _validator;


    public EditCourseCommandHandler(ICourseRepository courseRepository, IMapper mapper, IValidator<EditCourseCommand> validator)
    {
        _courseRepository = courseRepository;
        _mapper = mapper;
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
            var entity = _mapper.Map<CourseDbModel>(request);
            await _courseRepository.UpdateAsync(entity);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}