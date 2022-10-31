using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Courses.Domain.Entities;
using Courses.Infrastructure.Persistance.DbMap;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.Persistance;

public class CoursesDbContext : DbContext
{
	public DbSet<CourseDbModel> Courses { get; set; }
	public DbSet<CoursePurchasedDbModel> CoursePurchaseds { get; set; }
	public DbSet<CourseWishedDbModel> CourseWisheds { get; set; }

	public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
	{
		Database.Migrate();
	}
	


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new CourseDbMap());
        modelBuilder.ApplyConfiguration(new CoursePurchasedDbMap());
        modelBuilder.ApplyConfiguration(new CourseWishedDbMap());
    }

}
