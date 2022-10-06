namespace Identity.Application.Features.Services.Abstractions;

public interface IPaginationService<T>
{
    Task<(IQueryable<T>, int)> GetABatchOfData(IQueryable<T> items, int page, int pageSize);
}