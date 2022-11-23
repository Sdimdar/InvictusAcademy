using AutoMapper;
using FreeArticles.Domain.Entities;
using ServicesContracts.FreeArticles.Commands;
using ServicesContracts.FreeArticles.Models;
using ServicesContracts.FreeArticles.Queries;

namespace FreeArticles.Application.Mappings;

public class FreeArticleMapping : Profile
{
    public FreeArticleMapping()
    {
        CreateMap<CreateFreeArticleCommand, FreeArticleDbModel>();
        CreateMap<EditFreeArticleCommand, FreeArticleDbModel>();
        CreateMap<FreeArticleDbModel, FreeArticleVm>();
        CreateMap<EditFreeArticleCommand, GetFreeArticleDataQuery>();
    }
}