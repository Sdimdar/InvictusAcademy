using Courses.Domain.Entities;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Courses.Requests.Courses.Commands;
using ServicesContracts.Courses.Requests.Courses.Querries;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Requests.Tests.Commands;
using ServicesContracts.Courses.Requests.Tests.Queries;
using ServicesContracts.Courses.Responses;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class CoursesService : ICoursesService
{
    public ExtendedHttpClient<ICoursesService> ExtendedHttpClient { get; set; }

    public CoursesService(ExtendedHttpClient<ICoursesService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public async Task<DefaultResponseObject<CoursesVm>?> GetCoursesAsync(GetCoursesQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<GetCoursesQuery, DefaultResponseObject<CoursesVm>>(query, $"/Courses/GetCourses?UserId={query.UserId}&Type={query.Type}", cancellationToken);
    }

    public async Task<DefaultResponseObject<List<ShortModuleInfoVm>>> GetShortModulesInfoByCourseId(GetShortCourseInfoQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<List<ShortModuleInfoVm>>>($"/Modules/GetShortInfoByCourseId?CourseId={query.CourseId}", cancellationToken);
    }

    public async Task<DefaultResponseObject<List<ModuleInfoVm>>> GetModulesInfoByCourseId(GetFullByCourseIdQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient
            .GetAndReturnResponseAsync<DefaultResponseObject<List<ModuleInfoVm>>>($"/Modules/GetFullByCourseId?CourseId={query.CourseId}&UserId={query.UserId}", cancellationToken);
    }

    public async Task<DefaultResponseObject<CourseByIdVm>> GetCourseById(GetCourseByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<CourseByIdVm>>
            ($"/Course/GetCourse?id={query.Id}");
    }

    public async Task<DefaultResponseObject<bool>> AddToWishedCourse(AddToWishedCourseCommand query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<AddToWishedCourseCommand, DefaultResponseObject<bool>>
            (query, "/Course/Wished", cancellationToken);
    }

    public async Task<DefaultResponseObject<bool>> RemoveFromWishedCourse(RemoveFromWishedCommand request, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<RemoveFromWishedCommand, DefaultResponseObject<bool>>
            (request, "/Course/RemoveWished", cancellationToken);
    }

    public async Task<DefaultResponseObject<PurchasedCourseInfoVm>> GetPurchasedCourseInfo(GetPurchasedCourseDataQuery query,
                                                                                           CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.
            GetAndReturnResponseAsync<DefaultResponseObject<PurchasedCourseInfoVm>>($"Courses/GetPurchasedCourseData?CourseId={query.CourseId}&UserId={query.UserId}", cancellationToken);
    }

    public async Task<DefaultResponseObject<PurchasedArticleInfoVm>> GetPurchasedArticleInfo(GetPurchasedArticleQuery query, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.
            GetAndReturnResponseAsync<DefaultResponseObject<PurchasedArticleInfoVm>>($"Articles/GetPurchasedArticleInfo?" +
            $"UserId={query.UserId}&ModuleId={query.ModuleId}&CourseId={query.CourseId}&ArticleOrder={query.ArticleOrder}", cancellationToken);
    }

    public async Task<DefaultResponseObject<List<PurchasedTestVm>>> GetPurchasedTestInfo(GetPurchasedTestQuery request, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.
            GetAndReturnResponseAsync<DefaultResponseObject<List<PurchasedTestVm>>>($"Articles/GetPurchasedTestInfo?" +
        $"UserId={request.UserId}&ModuleId={request.ModuleId}&CourseId={request.CourseId}&ArticleOrder={request.ArticleOrder}", cancellationToken);
    }

    public async Task<DefaultResponseObject<TestResultVm>> CheckTestAnswer(CheckTestAnswersCommand request, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CheckTestAnswersCommand, DefaultResponseObject<TestResultVm>>
            (request, "/Tests/CheckTestAnswers", cancellationToken);
    }
}
