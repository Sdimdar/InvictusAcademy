using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    public partial class CitizenShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Citizenship",
                table: "Users",
                type: "VARCHAR(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Citizenship",
                table: "Users",
                type: "VARCHAR(60)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(60)",
                oldNullable: true);
        }
    }
}
