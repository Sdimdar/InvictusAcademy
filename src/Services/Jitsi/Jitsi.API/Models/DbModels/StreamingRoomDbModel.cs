using CommonRepository.Models;
using Courses.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Jitsi.API.Models.DbModels;

public class StreamingRoomDbModel : BaseRepositoryEntity
{
    public string Address { get; set; }
    public string Name { get; set; }
    public bool IsOpened { get; set; }
    public string ImageLink { get; set; }
}