using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;
using ServicesContracts.Payments.Response;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IPaymentService : IUseExtendedHttpClient<IPaymentService>
{
    Task<DefaultResponseObject<bool>> AddPaymentRequestAsync(AddPaymentCommand request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<bool>> ConfirmPaymentRequestAsync(ConfirmPaymentCommand request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<bool>> RejectPaymentRequestAsync(RejectPaymentCommand request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<PaymentVm>> GetByIdPaymentRequestAsync(GetPaymentQuery request, CancellationToken cancellationToken);
    Task<DefaultResponseObject<PaymentsPaginationVm>> GetWithParametersPaymentRequestAsync(GetPaymentsWithParametersQuery request, CancellationToken cancellationToken);

    Task<DefaultResponseObject<int>> GetPaymentsCount(GetPaymentsCountQuery request,
        CancellationToken cancellationToken);

    Task<DefaultResponseObject<List<PaymentHistoryVm>>> GetHistoryByAdminNameAsync(
        GetHistoryByAdminNameQuery request, CancellationToken cancellationToken);

    Task<DefaultResponseObject<List<PaymentHistoryVm>>> GetHistoryByPaymentId(
        GetHistoryByPaymentIdQuery request, CancellationToken cancellationToken);

    Task<DefaultResponseObject<bool>> CancelPaymentAsync(CancelPaymentCommand request,
        CancellationToken cancellationToken);
}