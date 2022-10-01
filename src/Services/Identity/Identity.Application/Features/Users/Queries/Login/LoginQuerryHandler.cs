using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Queries.Login;

public class LoginQuerryHandler : IRequestHandler<LoginQuerry, Result<LoginQuerryVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IValidator<LoginQuerry> _validator;
    private readonly IMapper _mapper;

    public LoginQuerryHandler(IUserRepository userRepository,
                              SignInManager<User> signInManager,
                              IValidator<LoginQuerry> validator,
                              IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<LoginQuerryVm>> Handle(LoginQuerry request, CancellationToken cancellationToken)
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
        if (result.Succeeded) return Result.Success(_mapper.Map<LoginQuerryVm>(user));
        return Result.Error("The Email password pair is not correct");
    }
}
