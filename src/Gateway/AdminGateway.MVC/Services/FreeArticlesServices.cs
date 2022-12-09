using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.FreeArticles.Commands;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;

namespace AdminGateway.MVC.Services;

public class FreeArticlesServices : IFreeArticlesService
{
    public ExtendedHttpClient<IFreeArticlesService> ExtendedHttpClient { get; set; }
    
    public FreeArticlesServices(ExtendedHttpClient<IFreeArticlesService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public async Task<DefaultResponseObject<string>> Create(CreateFreeArticleCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CreateFreeArticleCommand, DefaultResponseObject<string>>(request,$"/FreeArticle/Create");
    }

    public async Task<DefaultResponseObject<string>> Edit(EditFreeArticleCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<EditFreeArticleCommand, DefaultResponseObject<string>>(request,$"/FreeArticle/Edit");
    }

    public async Task<DefaultResponseObject<AllFreeArticlesVm>> GetAll(GetAllFreeArticlesQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<AllFreeArticlesVm>>($"/FreeArticle/GetAll?PageNumber={request.PageNumber}&PageSize={request.PageSize}&FilterString={request.FilterString}");
    }

    public async Task<DefaultResponseObject<FreeArticleVm>> GetFreeArticleData(GetFreeArticleDataQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<FreeArticleVm>>($"/FreeArticle/GetFreeArticleData?Id={request.Id}");
    }

    public async Task<DefaultResponseObject<int>> GetCount()
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<int>>($"/FreeArticle/GetCount");
    }
}