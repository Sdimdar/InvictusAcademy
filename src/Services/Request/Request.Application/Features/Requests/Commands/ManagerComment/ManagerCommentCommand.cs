using Ardalis.Result;
using MediatR;

namespace Request.Application.Features.Requests.Commands.ManagerComment;

public class ManagerCommentCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string ManagerComment { get; set; }
}