using CommonRepository.Models;

namespace Courses.Domain.Entities;

public class CoursePurchasedDbModel : BaseRepositoryEntity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public CourseDbModel Course { get; set; }
    public bool IsCompleted { get; set; }
}
