using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupKeyValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_payroll_employee_salary_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.CreateTable(
                name: "lookup_key_value",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lookup_key_value", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_employee_payroll_employee_salary_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll",
                column: "SalaryAccountId",
                principalSchema: "hrm",
                principalTable: "employee_salary_account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_payroll_employee_salary_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropTable(
                name: "lookup_key_value",
                schema: "lookup");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_payroll_employee_salary_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll",
                column: "SalaryAccountId",
                principalSchema: "hrm",
                principalTable: "employee_salary_account",
                principalColumn: "Id");
        }
    }
}
