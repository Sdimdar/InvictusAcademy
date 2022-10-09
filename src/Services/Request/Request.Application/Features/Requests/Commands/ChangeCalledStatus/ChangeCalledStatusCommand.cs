using Ardalis.Result;
using MediatR;

namespace Request.Application.Features.Requests.Commands.ChangeCalledStatus;

public class ChangeCalledStatusCommand : IRequest<Result>
{
    public int Id { get; set; }
}