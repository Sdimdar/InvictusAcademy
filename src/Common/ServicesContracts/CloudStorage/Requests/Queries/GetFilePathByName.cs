using Ardalis.Result;
using CloudStorage.Domain.Entities;
using MediatR;

namespace ServicesContracts.CloudStorage.Requests.Commands;

public class GetFilePathByName : IRequest<Result<CloudStorageDbModel>>
{
    public string FileName { get; set; }
}