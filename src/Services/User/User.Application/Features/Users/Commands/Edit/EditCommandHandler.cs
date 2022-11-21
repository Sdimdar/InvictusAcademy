using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.Identity.Requests.Commands;
using User.Application.Contracts;

namespace User.Application.Features.Users.Commands.Edit;

public class EditCommandHandler : IRequestHandler<EditCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<EditCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<EditCommandHandler> _logger;

    public EditCommandHandler(IValidator<EditCommand> validator, IUserRepository userRepository, IMapper mapper, ILogger<EditCommandHandler> logger)
    {
        _validator = validator;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result> Handle(EditCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = request.PhoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

        var result = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        if (result is null)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Email} email");
        }

        result.FirstName = request.FirstName;
        result.LastName = request.LastName;
        result.PhoneNumber = request.PhoneNumber;
        result.MiddleName = request.MiddleName;
        result.InstagramLink = request.InstagramLink;
        result.Citizenship = request.Citizenship;

        await _userRepository.UpdateAsync(result);
        return Result.Success();
    }
}