using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ServicesContracts.CloudStorage.Requests.Commands;

public class UploadFileCommand : IRequest<Result<string>>
{
    public IFormFile File { get; set; }
}