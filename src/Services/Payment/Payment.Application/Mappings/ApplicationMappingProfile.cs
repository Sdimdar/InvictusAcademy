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
		CreateMap<PaymentRequest, PaymentHistoryDbModel>()
			.ForMember(x => x.PaymentId, r => r.MapFrom(p => p.Id))
			.ForMember(x=>x.Id, opt => opt.Ignore());
	}
}
