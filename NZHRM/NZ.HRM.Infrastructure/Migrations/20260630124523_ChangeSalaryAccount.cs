using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSalaryAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_bank_account_banking_BankingId",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_bank_account_employee_master_EmployeeId",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_payroll_employee_bank_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employee_bank_account",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.RenameTable(
                name: "employee_bank_account",
                schema: "hrm",
                newName: "employee_salary_account",
                newSchema: "hrm");

            migrationBuilder.RenameIndex(
                name: "IX_employee_bank_account_EmployeeId",
                schema: "hrm",
                table: "employee_salary_account",
                newName: "IX_employee_salary_account_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_employee_bank_account_BankingId",
                schema: "hrm",
                table: "employee_salary_account",
                newName: "IX_employee_salary_account_BankingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employee_salary_account",
                schema: "hrm",
                table: "employee_salary_account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_payroll_employee_salary_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll",
                column: "SalaryAccountId",
                principalSchema: "hrm",
                principalTable: "employee_salary_account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_salary_account_banking_BankingId",
                schema: "hrm",
                table: "employee_salary_account",
                column: "BankingId",
                principalSchema: "lookup",
                principalTable: "banking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_salary_account_employee_master_EmployeeId",
                schema: "hrm",
                table: "employee_salary_account",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_payroll_employee_salary_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_salary_account_banking_BankingId",
                schema: "hrm",
                table: "employee_salary_account");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_salary_account_employee_master_EmployeeId",
                schema: "hrm",
                table: "employee_salary_account");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employee_salary_account",
                schema: "hrm",
                table: "employee_salary_account");

            migrationBuilder.RenameTable(
                name: "employee_salary_account",
                schema: "hrm",
                newName: "employee_bank_account",
                newSchema: "hrm");

            migrationBuilder.RenameIndex(
                name: "IX_employee_salary_account_EmployeeId",
                schema: "hrm",
                table: "employee_bank_account",
                newName: "IX_employee_bank_account_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_employee_salary_account_BankingId",
                schema: "hrm",
                table: "employee_bank_account",
                newName: "IX_employee_bank_account_BankingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employee_bank_account",
                schema: "hrm",
                table: "employee_bank_account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_bank_account_banking_BankingId",
                schema: "hrm",
                table: "employee_bank_account",
                column: "BankingId",
                principalSchema: "lookup",
                principalTable: "banking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_bank_account_employee_master_EmployeeId",
                schema: "hrm",
                table: "employee_bank_account",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_payroll_employee_bank_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll",
                column: "SalaryAccountId",
                principalSchema: "hrm",
                principalTable: "employee_bank_account",
                principalColumn: "Id");
        }
    }
}
