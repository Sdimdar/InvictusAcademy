using Ardalis.Result;
using FreeArticles.Application.Contracts;
using MediatR;
using ServicesContracts.FreeArticles.Queries;

namespace FreeArticles.Application.Features.Queries.GetFreeArticlesCount;

public class GetFreeArticlesCountQueryHandler : IRequestHandler<GetAllFreeArticlesCountQuery, Result<int>>
{
    private readonly IFreeArticleRepository _freeArticleRepository;

    public GetFreeArticlesCountQueryHandler(IFreeArticleRepository freeArticleRepository)
    {
        _freeArticleRepository = freeArticleRepository;
    }

    public async Task<Result<int>> Handle(GetAllFreeArticlesCountQuery request, CancellationToken cancellationToken)
    {
        return await _freeArticleRepository.GetCountAsync();
    }
}