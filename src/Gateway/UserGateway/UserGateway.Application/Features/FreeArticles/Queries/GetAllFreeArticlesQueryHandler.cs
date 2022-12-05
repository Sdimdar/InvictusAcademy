using Ardalis.Result;
using CommonStructures;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.FreeArticles.Queries;

public class GetAllFreeArticlesQueryHandler : IRequestHandler<GetAllFreeArticlesQuery, Result<AllFreeArticlesVm>>
{
    private readonly IFreeArticlesService _freeArticlesService;
    private readonly ILogger<GetAllFreeArticlesQueryHandler> _logger;

    public GetAllFreeArticlesQueryHandler(ILogger<GetAllFreeArticlesQueryHandler> logger, IFreeArticlesService freeArticlesService)
    {
        _logger = logger;
        _freeArticlesService = freeArticlesService;
    }

    public async Task<Result<AllFreeArticlesVm>> Handle(GetAllFreeArticlesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _freeArticlesService.GetAll(request, cancellationToken);
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