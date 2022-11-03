using Request.Application.Contracts;
using Request.Domain.Entities;
using TestCommonRepository;

namespace Request.API.Tests.Repository;

public class RequestMockRepository : TestCommonRepository<RequestDbModel>, IRequestRepository
{
    public RequestMockRepository()
    {
        Context = new List<RequestDbModel>()
        {
            new RequestDbModel()
            {
                Id = 1,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 2,
                PhoneNumber = "89348473402",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 3,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            },
            new RequestDbModel()
            {
                Id = 4,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                ManagerComment = "",
                UserName = "Famine",
                WasCalled = false
            }
        };
    }
    
    public override Task DeleteAsync(RequestDbModel entity)
    {
        if (Context.FirstOrDefault(e => e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }
    
    public override Task UpdateAsync(RequestDbModel entity)
    {
        if (Context.FirstOrDefault(e => e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }
    
    protected override IQueryable<RequestDbModel> FilterByString(IQueryable<RequestDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.UserName.ToLower().Contains(filterString.ToLower())
                            || v.PhoneNumber.ToLower().Contains(filterString.ToLower())
            );
    }
}
