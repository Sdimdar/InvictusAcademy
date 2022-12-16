using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Payment.Domain.Enums;
using Payment.Domain.Models;
using ServicesContracts.Payments.Response;

namespace ServicesContracts.Payments.Queries;

public class GetPaymentsWithParametersQuery : IRequest<Result<PaymentsPaginationVm>>
{
    public PaymentState Status { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}