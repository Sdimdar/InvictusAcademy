using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Payment.Domain.Models;
using Payment.Domain.Services;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using ServicesContracts.Payments.Response;

namespace Payment.Application.Features.Payments.Queries.GetPaymentsWithParameters;

public class GetPaymentsWithParametersHandler : IRequestHandler<GetPaymentsWithParametersQuery, Result<PaymentsPaginationVm>>
{
    private readonly PaymentService _paymentService;
    private readonly IValidator<GetPaymentsWithParametersQuery> _validator;
    private readonly ILogger<GetPaymentsWithParametersHandler> _logger;
    private readonly IMapper _mapper;
    

    public GetPaymentsWithParametersHandler(PaymentService paymentService, IValidator<GetPaymentsWithParametersQuery> validator, ILogger<GetPaymentsWithParametersHandler> logger, IMapper mapper)
    {
        _paymentService = paymentService;
        _validator = validator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<PaymentsPaginationVm>> Handle(GetPaymentsWithParametersQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        

        try
        {
            var paymentsCount =  _paymentService.GetPaymentsCount(request.Status);
            if (request.PageSize == 0)
            {
                request.PageNumber = 1;
                request.PageSize = await paymentsCount;
            }

            if (await paymentsCount == 0)
            {
                _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Payments list with status:{request.Status} is empty");
                return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Payments list with status:{request.Status} is empty");
            }

            var data = await _paymentService.GetPaymentsAsync(request.PageSize, request.PageNumber,request.Status);
            var response = new PaymentsPaginationVm
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Payments = _mapper.Map<List<PaymentsVm>>(data)
            };
            return Result.Success(response);
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