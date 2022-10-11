using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Identity.Application.Contracts;
using MediatR;

namespace Identity.Application.Features.Users.Commands.Edit;

public class EditCommandHandler : IRequestHandler<EditCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<EditCommand> _validator;

    public EditCommandHandler(IValidator<EditCommand> validator, IUserRepository userRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(EditCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        if (result is null) 
            return Result.Error("An error occurred while creating the request");

        result.MiddleName = request.MiddleName;
        result.InstagramLink = request.InstagramLink;
        result.Citizenship = request.Citizenship;

        await _userRepository.UpdateAsync(result);
        return Result.Success();
    }
}