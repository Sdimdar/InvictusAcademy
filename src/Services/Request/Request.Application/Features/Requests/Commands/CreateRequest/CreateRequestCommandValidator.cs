using FluentValidation;
using Request.Application.Contracts;

namespace Request.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
    private readonly IRequestRepository _requestRepository;
    public CreateRequestCommandValidator(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
        RuleFor(q => q.PhoneNumber)
            .MustAsync(IsNumberUnique).WithMessage("An request with this phone number has been registered. Manager will contact you soon")
            .NotEmpty().WithMessage("PhoneNumber is Required")
            .NotNull()
            .MaximumLength(13).WithMessage("PhoneNumber must be less than 13 characters long")
            .MinimumLength(11).WithMessage("PhoneNumber must be more than 11 characters long")
            .Must(phoneNumber => phoneNumber.All(Char.IsDigit)).WithMessage("PhoneNumber must contain only numbers");
        RuleFor(q => q.UserName)
            .NotEmpty().WithMessage("UserName is Required.")
            .NotNull();
    }

    private async Task<bool> IsNumberUnique(string phoneNumber,CancellationToken arg2)
    {
        return await _requestRepository.GetFirstOrDefaultAsync(r =>
            r.PhoneNumber.Equals(phoneNumber) && !r.WasCalled) is null;
    }
}