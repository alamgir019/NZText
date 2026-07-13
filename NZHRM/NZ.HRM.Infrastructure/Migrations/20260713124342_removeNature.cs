using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeNature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employment_employee_nature_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropTable(
                name: "employee_nature",
                schema: "lookup");

            migrationBuilder.DropIndex(
                name: "IX_employee_employment_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.RenameColumn(
                name: "EmployeeType",
                schema: "hrm",
                table: "employee_master",
                newName: "EmployeeNature");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeNature",
                schema: "hrm",
                table: "employee_master",
                newName: "EmployeeType");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "employee_nature",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    NatureName = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_nature", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeNatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employment_employee_nature_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeNatureId",
                principalSchema: "lookup",
                principalTable: "employee_nature",
                principalColumn: "Id");
        }
    }
}
