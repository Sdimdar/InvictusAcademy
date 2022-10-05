namespace Identity.Application.Features.Services.Abstractions;

public interface IFilter<T>
{
    IQueryable<T> Filter(IQueryable<T> items, string filterByName);
}