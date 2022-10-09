using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;
using PasswordsHash;
using System.Security.Claims;

namespace Identity.Application.Features.Users.Queries.Login;

public class LoginQuerryHandler : IRequestHandler<LoginQuerry, (List<Claim>?, Result<LoginQuerryVm>)>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginQuerry> _validator;
    private readonly IMapper _mapper;

    public LoginQuerryHandler(IUserRepository userRepository,
                              IValidator<LoginQuerry> validator,
                              IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<(List<Claim>?, Result<LoginQuerryVm>)> Handle(LoginQuerry request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return (null, Result.Invalid(validationResult.AsErrors()));
        }
        UserDbModel? user = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        if (user is null) return (null, Result.NotFound("User not found by email"));
        if (user.Password.VerifyHashedString(request.Password))
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            return (claims, Result.Success(_mapper.Map<LoginQuerryVm>(user)));
        }
        return (null, Result.Error("The Email password pair is not correct"));
    }
}
