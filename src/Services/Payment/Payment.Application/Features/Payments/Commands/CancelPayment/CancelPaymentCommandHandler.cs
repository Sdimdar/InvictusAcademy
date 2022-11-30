using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Models;
using Payment.Domain.Services;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.CancelPayment;

public class CancelPaymentCommandHandler:IRequestHandler<CancelPaymentCommand, Result<bool>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<CancelPaymentCommand> _validator;
    private readonly IPaymentHistoryRepository _paymentHistory;
    private readonly IMapper _mapper;

    public CancelPaymentCommandHandler(PaymentService paymentService, IValidator<CancelPaymentCommand> validator, IPaymentHistoryRepository paymentHistory, IMapper mapper)
    {
        _paymentService = paymentService;
        _validator = validator;
        _paymentHistory = paymentHistory;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request,cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var paymentRequest= await _paymentService.CancelPaymentAsync(request.PaymentId, request.RejectReason, request.AdminEmail);
            var paymentHistory = _mapper.Map<PaymentHistoryDbModel>(paymentRequest);
            await _paymentHistory.AddAsync(paymentHistory);
            return Result.Success(true);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
        catch (NullReferenceException ex)
        {
            return Result.Error("Payment with this Id not found");
        }
    }
}