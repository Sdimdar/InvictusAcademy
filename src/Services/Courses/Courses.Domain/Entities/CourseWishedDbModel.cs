using CommonRepository.Models;

namespace Courses.Domain.Entities;

public class CourseWishedDbModel : BaseRepositoryEntity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public CourseDbModel Course { get; set; }
}
