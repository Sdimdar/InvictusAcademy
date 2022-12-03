﻿using System.Net.Mime;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Edit;

public class EditCourseCommandHandler : IRequestHandler<EditCourseCommand,Result<string>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IValidator<EditCourseCommand> _validator;
    private readonly ILogger<EditCourseCommandHandler> _logger;
    private readonly IMapper _mapper;


    public EditCourseCommandHandler(ICourseRepository courseRepository, IValidator<EditCourseCommand> validator, ILogger<EditCourseCommandHandler> logger, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _validator = validator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(EditCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Course not found in database");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Course not found in database");
            }
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                return Result.Invalid(validationResult.AsErrors());
           
            course.Name = request.Name;
            course.Description = request.Description;
            course.SecondName = request.SecondName;
            course.SecondDescription = request.SecondDescription;
            course.CoursePoints = _mapper.Map<List<CoursePointsDbModel>>(request.CoursePoints);
            course.VideoLink = request.VideoLink;
            course.Cost = request.Cost;
            course.IsActive = request.IsActive;
            await _courseRepository.UpdateAsync(course);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}