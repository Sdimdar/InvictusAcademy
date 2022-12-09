using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using FreeArticles.Application.Contracts;
using FreeArticles.Application.Features.Commands.Create;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;

namespace FreeArticles.Application.Features.Queries.GetFreeArticleData;

public class GetFreeArticleDataHandler : IRequestHandler<GetFreeArticleDataQuery, Result<FreeArticleVm>>
{
    private readonly IMapper _mapper;
    private readonly IFreeArticleRepository _freeArticleRepository;
    private readonly IValidator<GetFreeArticleDataQuery> _validator;
    private readonly ILogger<CreateFreeArticleHandler> _logger;

    public GetFreeArticleDataHandler(IMapper mapper, IFreeArticleRepository freeArticleRepository, IValidator<GetFreeArticleDataQuery> validator, ILogger<CreateFreeArticleHandler> logger)
    {
        _mapper = mapper;
        _freeArticleRepository = freeArticleRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<FreeArticleVm>> Handle(GetFreeArticleDataQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            _logger.LogWarning($"{BussinesErrors.RequestIsNull.ToString()}: Request is null");
            return Result.Error($"{BussinesErrors.RequestIsNull.ToString()}: Request is null");
        }
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        try
        {
            var result = await _freeArticleRepository.GetFirstOrDefaultAsync(freeArticle=>freeArticle.Id == request.Id);
            if (result is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} ID");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} ID");
            }
            FreeArticleVm freeArticle = _mapper.Map<FreeArticleVm>(result);
            return Result.Success(freeArticle);
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
}