using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Request.Infrastructure.Migrations;

public partial class ChangeRequest : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "ManagerComment",
            table: "Requests",
            type: "VARCHAR(100)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "VARCHAR(13)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "LastModifiedDate",
            table: "Requests",
            type: "TIMESTAMP",
            nullable: false,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Requests",
            type: "TIMESTAMP",
            nullable: false,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "TIMESTAMP");

        migrationBuilder.CreateIndex(
            name: "IX_Requests_PhoneNumber",
            table: "Requests",
            column: "PhoneNumber");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Requests_PhoneNumber",
            table: "Requests");

        migrationBuilder.AlterColumn<string>(
            name: "ManagerComment",
            table: "Requests",
            type: "VARCHAR(13)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "VARCHAR(100)",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "LastModifiedDate",
            table: "Requests",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "TIMESTAMP",
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Requests",
            type: "TIMESTAMP",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "TIMESTAMP",
            oldDefaultValueSql: "NOW()");
    }
}
