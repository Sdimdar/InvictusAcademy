using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Payments.Commands;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Queries;

namespace UserGateway.Application.Contracts;

public interface IPaymentService : IUseExtendedHttpClient<IPaymentService>
{
    Task<DefaultResponseObject<bool>> AddPaymentRequestAsync(AddPaymentCommand request, CancellationToken cancellationToken);
}