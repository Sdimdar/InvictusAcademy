using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using PasswordsHash;
using System.Security.Claims;

namespace Identity.Application.Features.Users.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (List<Claim>?, Result<RegisterCommandVm>)>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository,
                                 IValidator<RegisterCommand> validator,
                                 IMapper mapper)
    {
        _userRepository = userRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<(List<Claim>?, Result<RegisterCommandVm>)> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = request.PhoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return (null, Result.Invalid(validationResult.AsErrors()));

        request.Password = request.Password.Hash();

        User user = _mapper.Map<User>(request);

        try
        {
            var result = await _userRepository.AddAsync(user);
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, result.Email)
            };
            return (claims ,Result.Success(_mapper.Map<RegisterCommandVm>(result)));
        }
        catch (Exception ex)
        {
            return (null, Result.Error(ex.Message));
        }
    }
}
