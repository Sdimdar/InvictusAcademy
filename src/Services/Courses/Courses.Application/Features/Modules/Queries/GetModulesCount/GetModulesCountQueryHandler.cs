using Ardalis.Result;
using Courses.Application.Contracts;
using MediatR;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetModulesCount;

public class GetModulesCountQueryHandler : IRequestHandler<GetModulesCountQuery, Result<int>>
{
    private readonly IModuleInfoRepository _moduleInfoRepository;

    public GetModulesCountQueryHandler(IModuleInfoRepository moduleInfoRepository)
    {
        _moduleInfoRepository = moduleInfoRepository;
    }

    public async Task<Result<int>> Handle(GetModulesCountQuery request, CancellationToken cancellationToken)
    {
        return await _moduleInfoRepository.GetCountAsync();
    }
}
