using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Request.Application.Contracts;
using Request.Domain.Entities;
using ServicesContracts.Request.Requests.Commands;

namespace Request.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, Result<string>>
{
    private readonly IMapper _mapper;
    private readonly IRequestRepository _requestRepository;
    private readonly IValidator<CreateRequestCommand> _validator;
    private readonly ILogger<CreateRequestHandler> _logger;

    public CreateRequestHandler(IMapper mapper, IRequestRepository requestRepository, IValidator<CreateRequestCommand> validator, ILogger<CreateRequestHandler> logger)
    {
        _mapper = mapper;
        _requestRepository = requestRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = request.PhoneNumber.Replace("(", "").Replace(")", "")
            .Replace("-", "").Replace(" ", "").Replace("+", "");
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        RequestDbModel newRequest = _mapper.Map<RequestDbModel>(request);
        var result = await _requestRepository.AddAsync(newRequest);
        if (result is null)
        {
            _logger.LogWarning($"{BussinesErrors.RequestIsNull.ToString()}: Request is Null");
            return Result.Error($"{BussinesErrors.RequestIsNull.ToString()}: Request is Null");
        }
        return Result.Success();
    }
}