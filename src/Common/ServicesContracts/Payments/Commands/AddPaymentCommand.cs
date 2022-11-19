using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Payments.Commands;

public class AddPaymentCommand : IRequest<Result<bool>>
{
    public string UserEmail { get; set; }
    public int CourseId { get; set; }
}