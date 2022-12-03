using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Payment.Infrastructure.Migrations;

public partial class Init : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "PaymentRequests",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UserId = table.Column<int>(type: "integer", nullable: false),
                CourseId = table.Column<int>(type: "integer", nullable: false),
                PaymentState = table.Column<int>(type: "integer", nullable: false),
                RejectReason = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                ModifyAdminEmail = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                LastModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PaymentRequests", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PaymentRequests_CourseId",
            table: "PaymentRequests",
            column: "CourseId");

        migrationBuilder.CreateIndex(
            name: "IX_PaymentRequests_UserId",
            table: "PaymentRequests",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PaymentRequests");
    }
}
