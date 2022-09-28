using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Queries.Login;

public class LoginQuerryHandler : IRequestHandler<LoginQuerry, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IValidator<LoginQuerry> _validator;

    public LoginQuerryHandler(IUserRepository userRepository,
                              SignInManager<User> signInManager,
                              IValidator<LoginQuerry> validator)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _validator = validator;
    }

    public async Task<Result<string>> Handle(LoginQuerry request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        User? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null) return Result.NotFound("User not found by email");
        SignInResult result = await _signInManager.PasswordSignInAsync(
            user,
            request.Password,
            request.RememberMe,
            false
        );
        if (result.Succeeded) return Result.Success("Login is sucsess");
        return Result.Error("The Email password pair is not correct");
    }
}
