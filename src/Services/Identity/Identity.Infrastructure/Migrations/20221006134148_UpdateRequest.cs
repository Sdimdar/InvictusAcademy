using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    public partial class UpdateRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ManagerComment",
                table: "Requests",
                type: "VARCHAR(13)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(13)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ManagerComment",
                table: "Requests",
                type: "VARCHAR(13)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(13)",
                oldNullable: true);
        }
    }
}
