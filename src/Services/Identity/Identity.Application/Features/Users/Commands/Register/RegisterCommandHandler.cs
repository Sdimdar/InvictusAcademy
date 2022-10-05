using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Features.Users.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterCommandVm>>
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository,
                                 SignInManager<User> signInManager,
                                 IValidator<RegisterCommand> validator,
                                 UserManager<User> userManager,
                                 IMapper mapper)
    {
        _userRepository = userRepository;
        _signInManager = signInManager;
        _validator = validator;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<RegisterCommandVm>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = request.PhoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
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
            return Result.Success(_mapper.Map<RegisterCommandVm>(user));
        }
        return Result.Error(result.Errors.Select(e => e.Description).ToArray());
    }
}
