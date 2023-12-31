﻿using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    public ExtendedHttpClient<IPaymentService> ExtendedHttpClient { get; set; }

    public PaymentService(ExtendedHttpClient<IPaymentService> httpClient)
    {
        ExtendedHttpClient = httpClient;
    }
    
    public async Task<DefaultResponseObject<bool>> AddPaymentRequestAsync(AddPaymentCommand request, 
                                                                     CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<AddPaymentCommand, DefaultResponseObject<bool>>
            (request, "/Payments/Add", cancellationToken);
    }

    public async Task<DefaultResponseObject<bool>> ConfirmPaymentRequestAsync(ConfirmPaymentCommand request, 
                                                                         CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<ConfirmPaymentCommand, DefaultResponseObject<bool>>
            (request, "/Payments/Confirm", cancellationToken);
    }

    public async Task<DefaultResponseObject<bool>> RejectPaymentRequestAsync(RejectPaymentCommand request, 
                                                                        CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<RejectPaymentCommand, DefaultResponseObject<bool>>
            (request, "/Payments/Reject", cancellationToken);
    }

    public async Task<DefaultResponseObject<PaymentVm>> GetByIdPaymentRequestAsync(GetPaymentQuery request, 
                                                                              CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<DefaultResponseObject<PaymentVm>>
            ($"/Payments/Get?PaymentId={request.PaymentId}", cancellationToken);
    }

   
}