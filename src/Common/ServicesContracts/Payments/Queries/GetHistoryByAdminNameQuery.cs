using Ardalis.Result;
using MediatR;
using Payment.Domain.Models;

namespace ServicesContracts.Payments.Queries;

public class GetHistoryByAdminNameQuery : IRequest<Result<List<PaymentHistoryDbModel?>>>
{
    public string AdminEmail { get; set; }
}