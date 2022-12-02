using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.Infrastructure.Migrations
{
    public partial class DeleteResultId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseResultId",
                table: "CoursePurchaseds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CourseResultId",
                table: "CoursePurchaseds",
                type: "numeric(7,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
