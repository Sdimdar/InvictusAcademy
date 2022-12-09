using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using FreeArticles.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using ServicesContracts.Identity.Responses;

namespace FreeArticles.Application.Features.Queries.GetAll;

public class GetAllFreeArticlesHandler : IRequestHandler<GetAllFreeArticlesQuery, Result<AllFreeArticlesVm>>
{
    private readonly IMapper _mapper;
    private readonly IFreeArticleRepository _freeArticleRepository;
    private readonly IValidator<GetAllFreeArticlesQuery> _validator;
    private readonly ILogger<GetAllFreeArticlesHandler> _logger;

    public GetAllFreeArticlesHandler(IMapper mapper, IFreeArticleRepository freeArticleRepository, IValidator<GetAllFreeArticlesQuery> validator, ILogger<GetAllFreeArticlesHandler> logger)
    {
        _mapper = mapper;
        _freeArticleRepository = freeArticleRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<AllFreeArticlesVm>> Handle(GetAllFreeArticlesQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            _logger.LogWarning($"{BussinesErrors.RequestIsNull.ToString()}: Request is null");
            return Result.Error($"{BussinesErrors.RequestIsNull.ToString()}: Request is null");
        }
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());
        try
        {
            var result = await _freeArticleRepository.GetFilteredBatchOfData(request.PageSize, request.PageNumber, request.FilterString);
            if (!result.Any())
            {
                _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: FreeArticles list is empty");
                return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: FreeArticles list is empty");
            }
            List<FreeArticleVm> allFreeArticles = _mapper.Map<List<FreeArticleVm>>(result);
            var response = new AllFreeArticlesVm()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                FreeArticles = allFreeArticles
            };
            return Result.Success(response);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
}