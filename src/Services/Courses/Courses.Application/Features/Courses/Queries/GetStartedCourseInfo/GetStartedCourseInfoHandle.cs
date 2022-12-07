using Ardalis.Result;
using AutoMapper;
using Courses.Application.Contracts;
using Courses.Domain.Entities;
using Courses.Domain.Entities.CourseResults;
using MediatR;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Responses;

namespace Courses.Application.Features.Courses.Queries.GetStartedCourseInfo;

public class GetStartedCourseInfoHandle:IRequestHandler<GetStartedCoursesQuery, Result<List<StartedCourseInfoVm>>>
{
    private readonly IMapper _mapper;
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseResultsInfoRepository _resultsInfoRepository;

    public GetStartedCourseInfoHandle(ICourseRepository courseRepository, ICourseResultsInfoRepository resultsInfoRepository, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _resultsInfoRepository = resultsInfoRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<StartedCourseInfoVm>>> Handle(GetStartedCoursesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<int> usersId = new List<int>();
            foreach (var item in request.ListOfId) usersId.Add(item.UserId);
            var purchasedCourses = await _courseRepository.GetPurchaseCourseByUserId(usersId);
            List<StartedCourseInfoVm> response = _mapper.Map<List<StartedCourseInfoVm>>(purchasedCourses);
            List<int> listOfId = new();
            foreach (var item in response) listOfId.Add(item.Id);
            //todo: чекнуть
            var resultInfo = await _resultsInfoRepository.GetInfoByListId(listOfId);
            foreach (var item in response)
            {
                CourseResultInfoDbModel? result = resultInfo.FirstOrDefault(x => x.Id == item.Id);
                if (result is not null)
                {
                    item.StartDate = result.StartDate.ToString("dd.MM.yyyy hh:mm");
                    item.EndDate = result.EndDate.ToString("dd.MM.yyyy hh:mm");
                }
            }
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}