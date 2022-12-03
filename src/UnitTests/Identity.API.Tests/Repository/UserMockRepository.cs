using User.Application.Contracts;
using User.Domain.Entities;

namespace User.API.Tests.Repository;

public class UserMockRepository : TestCommonRepository<UserDbModel>, IUserRepository
{
    public UserMockRepository()
    {
        Context = new List<UserDbModel>()
        {
            new UserDbModel()
            {
                Id = 1,
                AvatarLink = null,
                Citizenship = "Казахстан",
                Email = "test@mail.ru",
                FirstName = "Famine",
                MiddleName = "Famine",
                LastName = "Famine",
                InstagramLink = null,
                PhoneNumber = "82739348372",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Password = "ADUW344ryoKSA8iyKGjTgYWHVpTj1u9stDjCxRCj28Uppq0ujck4iv3gUkTMvrBRlQ==", // 123_QWEasd
                RegistrationDate = DateTime.Now
            },
            new UserDbModel()
            {
                Id = 2,
                AvatarLink = null,
                Citizenship = "Казахстан",
                Email = "test@test.ru",
                FirstName = "Famine",
                MiddleName = "Famine",
                LastName = "Famine",
                InstagramLink = null,
                PhoneNumber = "82739234234",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Password = "ADUW344ryoKSA8iyKGjTgYWHVpTj1u9stDjCxRCj28Uppq0ujck4iv3gUkTMvrBRlQ==", // 123_QWEasd
                RegistrationDate = DateTime.Now
            },
            new UserDbModel()
            {
                Id = 3,
                AvatarLink = null,
                Citizenship = "Казахстан",
                Email = "test@test.ru",
                FirstName = "Famine",
                MiddleName = "Famine",
                LastName = "Famine",
                InstagramLink = null,
                PhoneNumber = "82739234234",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Password = "ADUW344ryoKSA8iyKGjTgYWHVpTj1u9stDjCxRCj28Uppq0ujck4iv3gUkTMvrBRlQ==", // 123_QWEasd
                RegistrationDate = DateTime.Now
            },
            new UserDbModel()
            {
                Id = 4,
                AvatarLink = null,
                Citizenship = "Казахстан",
                Email = "test@test.ru",
                FirstName = "Famine",
                MiddleName = "Famine",
                LastName = "Famine",
                InstagramLink = null,
                PhoneNumber = "82739234234",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Password = "ADUW344ryoKSA8iyKGjTgYWHVpTj1u9stDjCxRCj28Uppq0ujck4iv3gUkTMvrBRlQ==", // 123_QWEasd
                RegistrationDate = DateTime.Now
            }
        };
    }

    public override Task DeleteAsync(UserDbModel entity)
    {
        if (Context.FirstOrDefault(e => e.Email == entity.Email
                                     || e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }

    public Task<List<UserDbModel>> GetUsersByIdList(List<int> usersId)
    {
        throw new NotImplementedException();
    }


    public override Task UpdateAsync(UserDbModel entity)
    {
        if (Context.FirstOrDefault(e => e.Email == entity.Email
                                     || e.PhoneNumber == entity.PhoneNumber) == null)
            throw new InvalidOperationException("User with this data is not exists");
        return Task.CompletedTask;
    }

    protected override IQueryable<UserDbModel> FilterByString(IQueryable<UserDbModel> query, string? filterString)
    {
        return string.IsNullOrEmpty(filterString)
            ? query
            : query.Where(v => v.FirstName.ToLower().Contains(filterString.ToLower())
                            || v.MiddleName!.ToLower().Contains(filterString.ToLower())
                            || v.LastName.ToLower().Contains(filterString.ToLower())
                            || v.PhoneNumber.ToLower().Contains(filterString.ToLower())
                            || v.Email.ToLower().Contains(filterString.ToLower())
                            || v.Citizenship!.ToLower().Contains(filterString.ToLower())
            );
    }
}
