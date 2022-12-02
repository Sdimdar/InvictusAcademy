using CommonRepository.Abstractions;
using FreeArticles.Domain.Entities;

namespace FreeArticles.Application.Contracts;

public interface IFreeArticleRepository: IBaseRepository<FreeArticleDbModel>
{
    
}