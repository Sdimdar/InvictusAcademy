using Ardalis.Result;
using MediatR;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.Payments.Commands.Add;

public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Result<bool>>
{
    private readonly IUserService _userService;
    private readonly IPaymentService _paymentService;

    public AddPaymentCommandHandler(IUserService userService, IPaymentService paymentService)
    {
        _userService = userService;
        _paymentService = paymentService;
    }

    public async Task<Result<bool>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
    {
        var userResponse = await _userService.GetUserAsync(request.UserEmal, cancellationToken);
        if (userResponse.IsSuccess)
        {
            ServicesContracts.Payments.Commands.AddPaymentCommand command = new()
            {
                UserEmail = request.UserEmal,
                CourseId = request.CourseId
            };
            var paymentResponse = await _paymentService.AddPaymentRequestAsync(command, cancellationToken);
            if (!paymentResponse.IsSuccess)
            {
                if (paymentResponse.Errors is not null)
                {
                    return Result.Error(paymentResponse.Errors);
                }
                return Result.Invalid(paymentResponse.ValidationErrors.ToList());
            }
            return Result.Success(paymentResponse.Value);
        }
        if (userResponse.Errors is not null)
        {
            return Result.Error(userResponse.Errors);
        }
        return Result.Invalid(userResponse.ValidationErrors.ToList());
    }
}