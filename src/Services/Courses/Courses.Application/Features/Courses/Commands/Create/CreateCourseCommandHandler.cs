using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.Create;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<string>>
{
    private readonly ICourseRepository _coursesRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCourseCommand> _validator;

    public CreateCourseCommandHandler(ICourseRepository coursesRepository,
                                      IMapper mapper,
                                      IValidator<CreateCourseCommand> validator)
    {
        _coursesRepository = coursesRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<string>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var entity = _mapper.Map<CourseDbModel>(request);
            await _coursesRepository.AddAsync(entity);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
