using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Articles.Commands.AddTest;

public class AddTestCommandHandler : IRequestHandler<AddTestCommand, Result<ModuleInfoVm>>
{
    private readonly IModuleInfoRepository _moduleInfoRepository;
    private readonly IValidator<AddTestCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<AddTestCommandHandler> _logger;

    public AddTestCommandHandler(IModuleInfoRepository moduleInfoRepository, IValidator<AddTestCommand> validator, IMapper mapper, ILogger<AddTestCommandHandler> logger)
    {
        _moduleInfoRepository = moduleInfoRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<ModuleInfoVm>> Handle(AddTestCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        
        var module = await _moduleInfoRepository.GetAsync(request.ModuleId, cancellationToken);
        var article = module!.Articles.First(a => a.Order == request.Order);

        article.Test = request.Test;

        await _moduleInfoRepository.UpdateAsync(request.ModuleId, module, cancellationToken);
        return Result.Success(_mapper.Map<ModuleInfoVm>(module));
    }
}