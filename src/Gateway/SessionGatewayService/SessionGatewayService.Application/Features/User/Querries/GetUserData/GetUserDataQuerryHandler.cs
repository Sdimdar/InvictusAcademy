using Ardalis.Result;
using AutoMapper;
using MediatR;
using SessionGatewayService.Application.Contracts;

namespace SessionGatewayService.Application.Features.User.Querries.GetUserData;

public class GetUserDataQuerryHandler : IRequestHandler<GetUserDataQuerry, Result<GetUserDataVm>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserDataQuerryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<Result<GetUserDataVm>> Handle(GetUserDataQuerry request, CancellationToken cancellationToken)
    {
        var Response = await _userService.GetUserAsync(request.Email, cancellationToken);
        var data = _mapper.Map<GetUserDataVm>(Response.Value);
        if (Response.IsSuccess) return Result.Success(data);
        if (Response.Errors.Count() != 0) return Result.Error(Response.Errors);
        return Result.Invalid(Response.ValidationErrors.ToList());
    }
}
