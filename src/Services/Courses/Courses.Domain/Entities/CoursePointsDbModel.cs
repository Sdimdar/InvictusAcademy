using CommonRepository.Models;

namespace Courses.Domain.Entities;

public class CoursePointsDbModel : BaseRepositoryEntity
{
    public string Point { get; set; }
    public string PointImageLink { get; set; }
    public int CourseId { get; set; }
    public CourseDbModel Course { get; set; }
}