using Ardalis.Result;
using CloudStorage.Domain.Entities;
using MediatR;

namespace ServicesContracts.CloudStorage.Requests.Queries;

public class GetFilesByFilterStringQuery : IRequest<Result<List<CloudStorageDbModel>?>>
{
    public string FilterString { get; set; }
}