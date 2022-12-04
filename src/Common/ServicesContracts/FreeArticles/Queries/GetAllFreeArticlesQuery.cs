using Ardalis.Result;
using MediatR;
using ServicesContracts.FreeArticles.Models;

namespace ServicesContracts.FreeArticles.Queries;

public class GetAllFreeArticlesQuery : IRequest<Result<AllFreeArticlesVm>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? FilterString { get; set; }
}