using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using FreeArticles.Application.Contracts;
using FreeArticles.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.FreeArticles.Commands;

namespace FreeArticles.Application.Features.Commands.Create;

public class CreateFreeArticleHandler : IRequestHandler<CreateFreeArticleCommand, Result<string>>
{
    private readonly IMapper _mapper;
    private readonly IFreeArticleRepository _freeArticleRepository;
    private readonly IValidator<CreateFreeArticleCommand> _validator;
    private readonly ILogger<CreateFreeArticleHandler> _logger;

    public CreateFreeArticleHandler(IMapper mapper,
        IFreeArticleRepository requestRepository,
        IValidator<CreateFreeArticleCommand> validator,
        ILogger<CreateFreeArticleHandler> logger)
    {
        _mapper = mapper;
        _freeArticleRepository = requestRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateFreeArticleCommand request, CancellationToken cancellationToken)
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
            FreeArticleDbModel newFreeArticle = _mapper.Map<FreeArticleDbModel>(request);
            newFreeArticle.IsVisible = true;
            var result = await _freeArticleRepository.AddAsync(newFreeArticle);
            if (result is null)
            {
                _logger.LogWarning($"{BussinesErrors.PostgresException.ToString()}: Failed to create in database");
                return Result.Error($"{BussinesErrors.PostgresException.ToString()}: Failed to create in database");
            }
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
}