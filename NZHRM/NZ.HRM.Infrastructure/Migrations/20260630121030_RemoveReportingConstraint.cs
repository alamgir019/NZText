using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReportingConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employment_employee_master_ReportingEmployeeId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropIndex(
                name: "IX_employee_employment_ReportingEmployeeId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "ReportingEmployeeId",
                schema: "hrm",
                table: "employee_employment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportingEmployeeId",
                schema: "hrm",
                table: "employee_employment",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_ReportingEmployeeId",
                schema: "hrm",
                table: "employee_employment",
                column: "ReportingEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employment_employee_master_ReportingEmployeeId",
                schema: "hrm",
                table: "employee_employment",
                column: "ReportingEmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id");
        }
    }
}
