using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Modules.Commands.UpdateArticle;

public class AddTestCommandHandler: IRequestHandler<AddTestCommand, Result<ModuleInfoVm>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IMapper _mapper;

    public AddTestCommandHandler(IModuleInfoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<ModuleInfoVm>> Handle(AddTestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var module = await _repository.GetAsync(request.ModuleId, cancellationToken);
            if (module is null) return Result.Error($"Module with Id: {request.ModuleId} not found");
            
            var article = module.Articles.FirstOrDefault(a => a.Order == request.Order);
            if (article != null) article.Test = request.Test;

            await _repository.UpdateAsync(request.ModuleId, module, cancellationToken);
            return Result.Success(_mapper.Map<ModuleInfoVm>(module));
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}