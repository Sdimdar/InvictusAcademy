using Ardalis.Result;
using CommonStructures;
using MediatR;
using Microsoft.Extensions.Logging;
using Request.Application.Contracts;
using ServicesContracts.Request.Requests.Commands;

namespace Request.Application.Features.Requests.Commands.ChangeCalledStatus;

public class ChangeCalledStatusHandler:IRequestHandler<ChangeCalledStatusCommand, Result<string>>
{
    private readonly IRequestRepository _requestRepository;
    private readonly ILogger<ChangeCalledStatusHandler> _logger;

    public ChangeCalledStatusHandler(IRequestRepository requestRepository, ILogger<ChangeCalledStatusHandler> logger)
    {
        _requestRepository = requestRepository;
        _logger = logger;
    }
    
    public async Task<Result<string>> Handle(ChangeCalledStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestRepository.GetFirstOrDefaultAsync(r => r.Id == request.Id);
        if (result == null)
        {
            _logger.LogWarning($"{BussinesErrors.DataIsNotExist.ToString()}: Data with this id does not exist");
            return Result.Error($"{BussinesErrors.DataIsNotExist.ToString()}: Data with this id does not exist");
        }

        if (result.WasCalled)
        {
            result.WasCalled = false;
            await _requestRepository.UpdateAsync(result);
            return Result.Success();
        }
        result.WasCalled = true;
        await _requestRepository.UpdateAsync(result);
        return Result.Success();
            
    }
}