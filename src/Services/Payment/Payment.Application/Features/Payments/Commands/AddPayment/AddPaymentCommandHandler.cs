using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using MediatR;
using Payment.Domain.Contracts;
using Payment.Domain.Models;
using Payment.Domain.Services;
using Payment.Infrastructure.Repositories;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.AddPayment;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Result<bool>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<AddPaymentCommand> _validator;
    private readonly IPaymentHistoryRepository _paymentHistory;
    private readonly IMapper _mapper;

    public AddPaymentCommandHandler(PaymentService paymentService, IValidator<AddPaymentCommand> validator, IPaymentHistoryRepository paymentHistory, IMapper mapper)
    {
        _paymentService = paymentService;
        _validator = validator;
        _paymentHistory = paymentHistory;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var paymentRequest = await _paymentService.AddPaymentRequestAsync(request.UserId, request.CourseId);
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