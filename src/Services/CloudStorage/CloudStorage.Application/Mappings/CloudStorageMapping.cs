using AutoMapper;
using CloudStorage.Domain.Entities;
using ServicesContracts.CloudStorage.Requests.Commands;

namespace CloudStorage.Application.Mappings;

public class CloudStorageMapping : Profile
{
    public CloudStorageMapping()
    {
        CreateMap<CloudStorageDbModel,UploadFileCommand>()
            .ForMember(m => m.File.FileName, 
                o => 
                    o.MapFrom(f => f.FileName));
    }
}