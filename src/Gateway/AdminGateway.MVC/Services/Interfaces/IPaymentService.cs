using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IPaymentService : IUseExtendedHttpClient<IPaymentService>
{
    Task<DefaultResponseObject<bool>> AddPaymentRequestAsync(AddPaymentCommand request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<bool>> ConfirmPaymentRequestAsync(ConfirmPaymentCommand request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<bool>> RejectPaymentRequestAsync(RejectPaymentCommand request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<PaymentVm>> GetByIdPaymentRequestAsync(GetPaymentQuery request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<List<PaymentVm>>> GetWithParametersPaymentRequestAsync(GetPaymentsWithParametersQuery request, CancellationToken cancellationToken);
}