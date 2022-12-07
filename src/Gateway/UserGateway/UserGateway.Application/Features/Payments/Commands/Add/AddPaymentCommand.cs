using Ardalis.Result;
using MediatR;

namespace UserGateway.Application.Features.Payments.Commands.Add;

public class AddPaymentCommand : IRequest<Result<bool>>
{
    public string UserEmail { get; set; }
    public int CourseId { get; set; }
}