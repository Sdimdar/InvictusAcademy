using Ardalis.Result;
using MediatR;
using ServicesContracts.FreeArticles.Models;

namespace ServicesContracts.FreeArticles.Queries;

public class GetFreeArticleDataQuery : IRequest<Result<FreeArticleVm>>
{
    public int Id { get; set; }
}