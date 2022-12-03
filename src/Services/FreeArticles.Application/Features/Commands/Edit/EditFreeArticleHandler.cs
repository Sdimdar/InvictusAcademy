using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using FreeArticles.Application.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using ServicesContracts.FreeArticles.Commands;

namespace FreeArticles.Application.Features.Commands.Edit;

public class EditFreeArticleHandler : IRequestHandler<EditFreeArticleCommand, Result<string>>
{
    private readonly IMapper _mapper;
    private readonly IFreeArticleRepository _freeArticleRepository;
    private readonly IValidator<EditFreeArticleCommand> _validator;
    private readonly ILogger<EditFreeArticleHandler> _logger;

    public EditFreeArticleHandler(IMapper mapper,
        IFreeArticleRepository freeArticleRepository,
        IValidator<EditFreeArticleCommand> validator,
        ILogger<EditFreeArticleHandler> logger)
    {
        _mapper = mapper;
        _freeArticleRepository = freeArticleRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(EditFreeArticleCommand request, CancellationToken cancellationToken)
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
            var result = await _freeArticleRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);
            if (result is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} ID");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} ID");
            }
            result.Title = request.Title;
            result.Text = request.Text;
            result.VideoLink = request.VideoLink;
            result.IsVisible = request.IsVisible;
            await _freeArticleRepository.UpdateAsync(result);
            return Result.Success();
        }
        catch (Exception e)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {e.Message}");
        }
    }
}