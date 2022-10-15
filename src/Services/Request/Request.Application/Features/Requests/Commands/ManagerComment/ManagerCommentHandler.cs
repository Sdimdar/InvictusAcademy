using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using Request.Application.Contracts;

namespace Request.Application.Features.Requests.Commands.ManagerComment;

public class ManagerCommentHandler: IRequestHandler<ManagerCommentCommand, Result>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IValidator<ManagerCommentCommand> _validator;

    public ManagerCommentHandler(IRequestRepository requestRepository, IValidator<ManagerCommentCommand> validator)
    {
        _requestRepository = requestRepository;
        _validator = validator;
    }

    public async Task<Result> Handle(ManagerCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestRepository.GetFirstOrDefaultAsync(r => r.Id == request.Id);
        if(result == null)
            return Result.Error("Data with this id does not exist");
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Invalid(validationResult.AsErrors());
        }
        result.ManagerComment = request.ManagerComment;
        await _requestRepository.UpdateAsync(result);
        return Result.Success();
    }
}