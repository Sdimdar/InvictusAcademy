using AutoMapper;
using Payment.Domain.Models;
using Payment.Infrastructure.Persistence.Models;

namespace Payment.Infrastructure.Mappings;

public class DbMappingProfile : Profile
{
    public DbMappingProfile()
    {
        CreateMap<PaymentRequest, PaymentRequestDbModel>().ReverseMap();
    }
}