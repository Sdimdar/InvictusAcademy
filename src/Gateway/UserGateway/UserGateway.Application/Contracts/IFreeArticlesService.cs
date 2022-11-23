using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;

namespace UserGateway.Application.Contracts;

public interface IFreeArticlesService : IUseExtendedHttpClient<IFreeArticlesService>
{
    Task<DefaultResponseObject<AllFreeArticlesVm>> GetAll(GetAllFreeArticlesQuery request,
        CancellationToken cancellationToken);
    Task<DefaultResponseObject<FreeArticleVm>> GetFreeArticleData(GetFreeArticleDataQuery request, CancellationToken cancellationToken);
}