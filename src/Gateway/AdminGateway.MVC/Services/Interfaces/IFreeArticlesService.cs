using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.FreeArticles.Commands;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IFreeArticlesService : IUseExtendedHttpClient<IFreeArticlesService>
{
    Task<DefaultResponseObject<string>> Create(CreateFreeArticleCommand request);
    Task<DefaultResponseObject<string>> Edit(EditFreeArticleCommand request);
    Task<DefaultResponseObject<AllFreeArticlesVm>> GetAll(GetAllFreeArticlesQuery request);
    Task<DefaultResponseObject<FreeArticleVm>> GetFreeArticleData(GetFreeArticleDataQuery request);
    Task<DefaultResponseObject<int>> GetCount();
}