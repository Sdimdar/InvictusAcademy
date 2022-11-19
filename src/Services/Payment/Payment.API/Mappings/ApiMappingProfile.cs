using AutoMapper;
using Payment.Domain.Models;
using ServicesContracts.Payments.Models;

namespace Payment.API.Mappings;

public class ApiMappingProfile : Profile
{
	public ApiMappingProfile()
	{
		CreateMap<PaymentRequest, PaymentVm>();
    }
}
