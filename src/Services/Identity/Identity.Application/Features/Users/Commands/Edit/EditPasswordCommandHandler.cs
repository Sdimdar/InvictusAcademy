using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Identity.Application.Contracts;
using MediatR;
using PasswordsHash;
using ServicesContracts.Identity.Requests.Commands;

namespace Identity.Application.Features.Users.Commands.Edit;

public class EditPasswordCommandHandler : IRequestHandler<EditPasswordCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<EditPasswordCommand> _validator;

    public EditPasswordCommandHandler(IUserRepository userRepository, IValidator<EditPasswordCommand> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }


    public async Task<Result> Handle(EditPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());
        
        if (result is null) 
            return Result.Error("An error occurred while creating the request");
        
        if(!result.Password.VerifyHashedString(request.OldPassword))
            return Result.Error("Неверно введен старый пароль. Повторите попытку");
        
        if(!request.NewPassword.Equals(request.ConfirmPassword))
            return Result.Error("Пароли не совпадают. Повторите попытку");
        
        result.Password = request.NewPassword.Hash();
        
        await _userRepository.UpdateAsync(result);
        return Result.Success();

    }
}