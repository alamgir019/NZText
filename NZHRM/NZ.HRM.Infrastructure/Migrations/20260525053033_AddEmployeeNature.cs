using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeNature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNature",
                table: "EmployeeMasters");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNatureId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeNatures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    NatureName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeNatures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_EmployeeNatureId",
                table: "EmployeeMasters",
                column: "EmployeeNatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_EmployeeNatures_EmployeeNatureId",
                table: "EmployeeMasters",
                column: "EmployeeNatureId",
                principalTable: "EmployeeNatures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_EmployeeNatures_EmployeeNatureId",
                table: "EmployeeMasters");

            migrationBuilder.DropTable(
                name: "EmployeeNatures");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMasters_EmployeeNatureId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "EmployeeNatureId",
                table: "EmployeeMasters");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeNature",
                table: "EmployeeMasters",
                type: "integer",
                nullable: true);
        }
    }
}
