﻿using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Create;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(p => p.Cost)
            .NotEmpty().WithMessage("Cost is required")
            .NotNull()
            .LessThan(9999999999999.99m).WithMessage("Cost can't be much big")
            .GreaterThan(0).WithMessage("Cost can't be smaller then 0");
        RuleFor(p => p.VideoLink)
            .MaximumLength(100).WithMessage("Video Link can't be longer then 100 symbols");
        RuleFor(p => p.LogoImageLink)
            .MaximumLength(100).WithMessage("Logo image Link can't be longer then 100 symbols");
        RuleFor(p => p.PreviewLink)
            .MaximumLength(100).WithMessage("Preview Link can't be longer then 100 symbols");
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull()
            .MaximumLength(100).WithMessage("Video Link can't be longer then 100 symbols");
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull()
            .MaximumLength(500).WithMessage("Video Link can't be longer then 500 symbols");
        RuleFor(p => p.PassingDayCount)
            .GreaterThan(0).WithMessage("Course can't be passing less then 1 day");
    }
}
