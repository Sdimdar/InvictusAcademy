using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Courses.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false, defaultValue: ""),
                    Description = table.Column<string>(type: "VARCHAR(500)", nullable: false, defaultValue: ""),
                    VideoLink = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()"),
                    LastModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoursePointsDbModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Point = table.Column<string>(type: "text", nullable: false),
                    PointImageLink = table.Column<string>(type: "text", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePointsDbModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursePointsDbModel_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursePurchaseds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<decimal>(type: "numeric(7,0)", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CourseResultId = table.Column<decimal>(type: "numeric(7,0)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "BOOLEAN", nullable: false, defaultValue: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()"),
                    LastModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePurchaseds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursePurchaseds_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseWisheds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<decimal>(type: "numeric(7,0)", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()"),
                    LastModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseWisheds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseWisheds_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursePointsDbModel_CourseId",
                table: "CoursePointsDbModel",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePurchaseds_CourseId",
                table: "CoursePurchaseds",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseWisheds_CourseId",
                table: "CourseWisheds",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursePointsDbModel");

            migrationBuilder.DropTable(
                name: "CoursePurchaseds");

            migrationBuilder.DropTable(
                name: "CourseWisheds");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
