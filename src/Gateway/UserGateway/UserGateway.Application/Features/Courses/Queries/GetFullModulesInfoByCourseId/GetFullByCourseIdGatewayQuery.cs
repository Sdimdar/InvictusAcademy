using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Features.Courses.Queries.GetFullModulesInfoByCourseId;

public class GetFullByCourseIdGatewayQuery : IRequest<Result<List<ModuleInfoVm>>>
{
    public int CourseId { get; set; }
    public string? UserEmail { get; set; }
}