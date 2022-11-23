using Ardalis.Result;
using CommonStructures;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.FreeArticles.Queries;

public class GetFreeArticleDataQueryHandler : IRequestHandler<GetFreeArticleDataQuery, Result<FreeArticleVm>>
{
    private readonly IFreeArticlesService _freeArticlesService;
    private readonly ILogger<GetFreeArticleDataQueryHandler> _logger;

    public GetFreeArticleDataQueryHandler(IFreeArticlesService freeArticlesService, ILogger<GetFreeArticleDataQueryHandler> logger)
    {
        _freeArticlesService = freeArticlesService;
        _logger = logger;
    }

    public async Task<Result<FreeArticleVm>> Handle(GetFreeArticleDataQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _freeArticlesService.GetFreeArticleData(request, cancellationToken);
            if (response.IsSuccess)
                return Result.Success(response.Value);

            if (response.Errors.Count() != 0)
            {
                return Result.Error(response.Errors);
            }
            return Result.Invalid(response.ValidationErrors.ToList());
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
}