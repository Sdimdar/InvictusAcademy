using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Queries.Register;

public class RegisterQuerryHandler : IRequestHandler<RegisterQuerry, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IValidator<RegisterQuerry> _validator;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public RegisterQuerryHandler(IUserRepository userRepository,
                                 SignInManager<User> signInManager,
                                 IValidator<RegisterQuerry> validator,
                                 UserManager<User> userManager,
                                 IMapper mapper)
    {
        _userRepository = userRepository;
        _signInManager = signInManager;
        _validator = validator;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(RegisterQuerry request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        User? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user != null) return Result.Error("A user with this Email exists");
        user = _mapper.Map<User>(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Result.SuccessWithMessage($"You sucsessful register with email: {user.Email}");
        }
        return Result.Error(result.Errors.Select(e => e.Description).ToArray());
    }
}
