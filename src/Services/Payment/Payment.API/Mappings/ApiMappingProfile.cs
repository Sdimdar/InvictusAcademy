using AutoMapper;
using Payment.Domain.Models;
using ServicesContracts.Payments.Models;
using ServicesContracts.Payments.Response;

namespace Payment.API.Mappings;

public class ApiMappingProfile : Profile
{
    
	public ApiMappingProfile()
	{
		CreateMap<PaymentRequest, PaymentVm>();
		CreateMap<PaymentRequest, PaymentsVm>();
		CreateMap<PaymentHistoryDbModel, PaymentHistoryVm>()
			.ForMember(p=>p.CreatedDate, opt=> opt.MapFrom(x=> x.CreatedDate.ToString("dd.MM.yyyy hh:mm")));
	}
}
