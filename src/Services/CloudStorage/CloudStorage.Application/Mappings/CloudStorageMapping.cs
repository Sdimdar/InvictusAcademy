using AutoMapper;
using CloudStorage.Domain.Entities;
using ServicesContracts.CloudStorage.Requests.Commands;
using ServicesContracts.CloudStorage.Requests.Querries;
using ServicesContracts.CloudStorage.Responses;

namespace CloudStorage.Application.Mappings;

public class CloudStorageMapping : Profile
{
    public CloudStorageMapping()
    {
        CreateMap<GetAllFilesQuery, GetAllFilesVM>();
    }
}