using CommonRepository.Models;
using Courses.Domain.Entities;

namespace Jitsi.API.Models.DbModels;

public class StreamingRoomDbModel : BaseRepositoryEntity
{
    public string Address { get; set; }
    public string Name { get; set; }
    public bool IsOpened { get; set; }
}