using Ardalis.Result;
using CommonStructures;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.FreeArticles.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.FreeArticles.Queries;

public class GetFreeArticlesCountQueryHandler : IRequestHandler<GetAllFreeArticlesCountQuery, Result<int>>
{
    private readonly IFreeArticlesService _freeArticlesService;
    private readonly ILogger<GetFreeArticlesCountQueryHandler> _logger;

    public GetFreeArticlesCountQueryHandler(IFreeArticlesService freeArticlesService, ILogger<GetFreeArticlesCountQueryHandler> logger)
    {
        _freeArticlesService = freeArticlesService;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(GetAllFreeArticlesCountQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _freeArticlesService.GetCount(cancellationToken);
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