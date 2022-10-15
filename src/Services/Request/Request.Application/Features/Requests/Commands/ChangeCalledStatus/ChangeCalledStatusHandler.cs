using Ardalis.Result;
using MediatR;
using Request.Application.Contracts;

namespace Request.Application.Features.Requests.Commands.ChangeCalledStatus;

public class ChangeCalledStatusHandler:IRequestHandler<ChangeCalledStatusCommand, Result>
{
    private readonly IRequestRepository _requestRepository;

    public ChangeCalledStatusHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }
    
    public async Task<Result> Handle(ChangeCalledStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestRepository.GetFirstOrDefaultAsync(r => r.Id == request.Id);
        if(result == null)
            return Result.Error("Data with this id does not exist");
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