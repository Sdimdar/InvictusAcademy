using CommonRepository;
using FreeArticles.Application.Contracts;
using FreeArticles.Domain.Entities;
using FreeArticles.Infrastructure.Persistence;

namespace FreeArticles.Infrastructure.Repositories;

public class FreeArticleRepository : BaseRepository<FreeArticleDbModel, FreeArticleDbContext>, IFreeArticleRepository
{
    public FreeArticleRepository(FreeArticleDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<FreeArticleDbModel> FilterByString(IQueryable<FreeArticleDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.Title.ToLower().Contains(filterString.ToLower())
            );
    }
}