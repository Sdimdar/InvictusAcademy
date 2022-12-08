using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ServicesContracts.CloudStorage.Requests.Commands;

public class UploadFileCommand : IRequest<Result<string>>
{
    public string FilePath { get; set; }
    public string FileName { get; set; }
}