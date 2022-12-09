using Ardalis.Result;
using MediatR;

namespace ServicesContracts.FreeArticles.Queries;

public class GetAllFreeArticlesCountQuery  : IRequest<Result<int>>
{
    
}