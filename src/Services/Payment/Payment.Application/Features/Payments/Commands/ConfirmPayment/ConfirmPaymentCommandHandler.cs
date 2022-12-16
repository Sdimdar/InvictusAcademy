using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Models;
using Microsoft.Extensions.Logging;
using Payment.Domain.Services;
using Payment.Infrastructure.Repositories;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.ConfirmPayment;

public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, Result<bool>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<ConfirmPaymentCommand> _validator;
    private readonly IPaymentHistoryRepository _paymentHistory;
    private readonly IMapper _mapper;
    private readonly ILogger<ConfirmPaymentCommand> _logger;

    public ConfirmPaymentCommandHandler(PaymentService paymentService, IValidator<ConfirmPaymentCommand> validator, IPaymentHistoryRepository paymentHistory, IMapper mapper, ILogger<ConfirmPaymentCommand> logger)
    {
        _paymentService = paymentService;
        _validator = validator;
        _paymentHistory = paymentHistory;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var paymentRequest = await _paymentService.AcceptPaymentAsync(request.PaymentId, request.AdminEmail);
            var paymentHistory = _mapper.Map<PaymentHistoryDbModel>(paymentRequest);
            await _paymentHistory.AddAsync(paymentHistory);
            return Result.Success(true);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning($"{BussinesErrors.InvalidCastException.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.InvalidCastException.ToString()}: {ex.Message}");
        }
        catch (NullReferenceException ex)
        {
            _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Payment with this Id not found");
            return Result.Error($"{BussinesErrors.NotFound.ToString()}: Payment with this Id not found");;
        }
    }
}