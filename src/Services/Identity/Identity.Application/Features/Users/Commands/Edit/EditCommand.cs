using Ardalis.Result;
using MediatR;

namespace Identity.Application.Features.Users.Commands.Edit;

public class EditCommand : IRequest<Result>
{
    public string Email { get; set; }
    public string? MiddleName { get; set; }
    public string? InstagramLink { get; set; }
    public string? Citizenship { get; set; }
    
}