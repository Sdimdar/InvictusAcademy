using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Infrastructure.Services;

public class FreeArticlesService : IFreeArticlesService
{
    public ExtendedHttpClient<IFreeArticlesService> ExtendedHttpClient { get; set; }

    public FreeArticlesService(ExtendedHttpClient<IFreeArticlesService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public async Task<DefaultResponseObject<AllFreeArticlesVm>> GetAll(GetAllFreeArticlesQuery request,
        CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<AllFreeArticlesVm>>($"/FreeArticle/GetAll?PageNumber={request.PageNumber}&PageSize={request.PageSize}&FilterString={request.FilterString}", cancellationToken);
    }

    public async Task<DefaultResponseObject<FreeArticleVm>> GetFreeArticleData(GetFreeArticleDataQuery request, CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<FreeArticleVm>>($"/FreeArticle/GetFreeArticleData?Id={request.Id}", cancellationToken);
    }

    public async Task<DefaultResponseObject<int>> GetCount(CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<int>>($"/FreeArticle/GetCount", cancellationToken);
    }
}