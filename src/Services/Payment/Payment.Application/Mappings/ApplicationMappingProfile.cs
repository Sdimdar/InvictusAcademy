using AutoMapper;
using Payment.Domain.Models;
using ServicesContracts.Payments.Models;

namespace Payment.Application.Mappings;

public class ApplicationMappingProfile : Profile
{
	public ApplicationMappingProfile()
	{
		CreateMap<PaymentRequest, PaymentVm>();
		CreateMap<PaymentRequest, PaymentsVm>();
    }
}
