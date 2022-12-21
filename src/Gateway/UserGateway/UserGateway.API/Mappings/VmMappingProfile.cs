using AutoMapper;
using FreeArticles.Domain.Entities;
using ServicesContracts.FreeArticles.Models;

namespace UserGateway.API.Mappings;

public class VmMappingProfile : Profile
{
	public VmMappingProfile()
	{
		CreateMap<FreeArticleVm, FreeArticleShortVm>();
		CreateMap<AllFreeArticlesVm, AllFreeArticlesShortVm>();
	}
}
