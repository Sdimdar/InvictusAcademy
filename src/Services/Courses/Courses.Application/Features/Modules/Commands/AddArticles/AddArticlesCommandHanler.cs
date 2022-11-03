using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using Courses.Application.Contracts;
using Courses.Domain.Entities.CourseInfo;
using FluentValidation;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Modules.Commands.AddArticles;

public class AddArticlesCommandHanler : IRequestHandler<AddArticlesCommand, Result<ModuleInfoVm>>
{
    private readonly IModuleInfoRepository _repository;
    private readonly IValidator<AddArticlesCommand> _validator;
    private readonly IMapper _mapper;

    public AddArticlesCommandHanler(IModuleInfoRepository repository,
                                      IValidator<AddArticlesCommand> validator,
                                      IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<Result<ModuleInfoVm>> Handle(AddArticlesCommand request, CancellationToken cancellationToken)
    {
        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(validatorResult.AsErrors());
        }
        try
        {
            var module = await _repository.GetAsync(request.ModuleId, cancellationToken);
            if (module is null) return Result.Error($"Module with Id: {request.ModuleId} not found");
            module.Articles = request.Articles;
            await _repository.UpdateAsync(request.ModuleId, module, cancellationToken);
            return Result.Success(_mapper.Map<ModuleInfoVm>(module));
        }
        catch (InvalidOperationException ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
