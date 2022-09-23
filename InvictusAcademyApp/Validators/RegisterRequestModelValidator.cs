using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using InvictusAcademyApp.Models.RequestModels;

namespace InvictusAcademyApp.Validators;

public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestModelValidator()
    {
        RuleFor(s => s.Email).NotEmpty().WithMessage("Поле обязательно для запонения");
        RuleFor(s => s.Password).NotEmpty().WithMessage("Поле обязательно для запонения");
        RuleFor(s => s.FirstName).NotEmpty().WithMessage("Поле обязательно для запонения");
        RuleFor(s => s.LastName).NotEmpty().WithMessage("Поле обязательно для запонения");
        RuleFor(s => s.PhoneNumber).NotEmpty().WithMessage("Поле обязательно для запонения");
        RuleFor(s => s.Citizenship).NotEmpty().WithMessage("Поле обязательно для запонения");
        
    }
}