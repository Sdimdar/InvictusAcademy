using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Request.Requests.Querries;

public class GetRequestsCountQuerry : IRequest<Result<int>>
{
}
