﻿using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using Identity.Application.Contracts;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestHandler : IRequestHandler<CreateRequestCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IRequestRepository _requestRepository;
    private readonly IValidator<CreateRequestCommand> _validator;

    public CreateRequestHandler(IMapper mapper, IRequestRepository requestRepository, IValidator<CreateRequestCommand> validator)
    {
        _mapper = mapper;
        _requestRepository = requestRepository;
        _validator = validator;
    }

    public async Task<Result> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        request.PhoneNumber = request.PhoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        Request newRequest = _mapper.Map<Request>(request);
        var result = await _requestRepository.CreateAsync(newRequest);
        if (result is not null)
        {
            return Result.Success();
        }

        return Result.Error("An error occurred while creating the request");
    }
}