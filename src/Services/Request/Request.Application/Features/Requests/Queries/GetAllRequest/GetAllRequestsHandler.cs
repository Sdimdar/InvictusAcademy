using Ardalis.Result;
using AutoMapper;
using MediatR;
using Request.Application.Contracts;

namespace Request.Application.Features.Requests.Queries.GetAllRequest;

public class GetAllRequestsHandler:IRequestHandler<GetAllRequestCommand,Result<GetAllRequestVm>>
{
    private readonly IMapper _mapper;
    private readonly IRequestRepository _requestRepository;


    public GetAllRequestsHandler(IMapper mapper, IRequestRepository requestRepository)
    {
        _mapper = mapper;
        _requestRepository = requestRepository;
    }

    public async Task<Result<GetAllRequestVm>> Handle(GetAllRequestCommand request, CancellationToken cancellationToken)
    {
        var result = await _requestRepository.GetRequestsByPage(request);
        if (!result.Any())
        {
            return Result.Error("Request list is empty");
        }

        var response = new GetAllRequestVm
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Requests = result
        };
        return Result.Success(response);
    }
}