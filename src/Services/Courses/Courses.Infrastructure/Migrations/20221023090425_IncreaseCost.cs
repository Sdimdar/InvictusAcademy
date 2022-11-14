using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.Infrastructure.Migrations
{
    public partial class IncreaseCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Courses",
                type: "numeric(15,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Courses",
                type: "numeric(7,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(15,2)");
        }
    }
}
