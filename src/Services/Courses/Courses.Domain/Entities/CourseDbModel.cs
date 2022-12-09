using CommonRepository.Models;

namespace Courses.Domain.Entities;

public class CourseDbModel : BaseRepositoryEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SecondName { get; set; }
    public string SecondDescription { get; set; }
    public string? VideoLink { get; set; }
    public decimal Cost { get; set; }
    public bool IsActive { get; set; }
    public int PassingDayCount { get; set; }
    public ICollection<CoursePointsDbModel> CoursePoints { get; set; }
    public ICollection<CoursePurchasedDbModel> CoursePurchased { get; set; }
    public ICollection<CourseWishedDbModel> CourseWished { get; set; }
}
