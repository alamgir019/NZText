using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmployeeDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employment_holiday_calendar_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropForeignKey(
                name: "FK_mst_subunit_district_DistrictId",
                schema: "master",
                table: "mst_subunit");

            migrationBuilder.DropTable(
                name: "bank",
                schema: "lookup");

            migrationBuilder.DropIndex(
                name: "IX_employee_employment_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "BankId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                schema: "hrm",
                table: "employee_document");

            migrationBuilder.DropColumn(
                name: "MobileBankingFlag",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                schema: "master",
                table: "mst_subunit",
                newName: "LookDistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_mst_subunit_DistrictId",
                schema: "master",
                table: "mst_subunit",
                newName: "IX_mst_subunit_LookDistrictId");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                schema: "hrm",
                table: "employee_document",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "BankId",
                schema: "hrm",
                table: "employee_bank_account",
                newName: "AccountType");

            migrationBuilder.AlterColumn<string>(
                name: "OtherAllowance",
                schema: "hrm",
                table: "employee_payroll",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BankPortion",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CashPortion",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProposedSalary",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProbationPeriod",
                schema: "hrm",
                table: "employee_employment",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingTo",
                schema: "hrm",
                table: "employee_employment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeeklyOffDay",
                schema: "hrm",
                table: "employee_employment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankingId",
                schema: "hrm",
                table: "employee_bank_account",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                schema: "hrm",
                table: "employee_bank_account",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "banking",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    BankingCode = table.Column<string>(type: "text", nullable: false),
                    BankingName = table.Column<string>(type: "text", nullable: false),
                    MobileBankingFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banking", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_payroll_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll",
                column: "SalaryAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_bank_account_BankingId",
                schema: "hrm",
                table: "employee_bank_account",
                column: "BankingId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_bank_account_banking_BankingId",
                schema: "hrm",
                table: "employee_bank_account",
                column: "BankingId",
                principalSchema: "lookup",
                principalTable: "banking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_payroll_employee_bank_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll",
                column: "SalaryAccountId",
                principalSchema: "hrm",
                principalTable: "employee_bank_account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_subunit_district_LookDistrictId",
                schema: "master",
                table: "mst_subunit",
                column: "LookDistrictId",
                principalSchema: "lookup",
                principalTable: "district",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_bank_account_banking_BankingId",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_payroll_employee_bank_account_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropForeignKey(
                name: "FK_mst_subunit_district_LookDistrictId",
                schema: "master",
                table: "mst_subunit");

            migrationBuilder.DropTable(
                name: "banking",
                schema: "lookup");

            migrationBuilder.DropIndex(
                name: "IX_employee_payroll_SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropIndex(
                name: "IX_employee_bank_account_BankingId",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.DropColumn(
                name: "BankPortion",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "CashPortion",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "ProposedSalary",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "SalaryAccountId",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "ProbationPeriod",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "ReportingTo",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "WeeklyOffDay",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "BankingId",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                schema: "hrm",
                table: "employee_bank_account");

            migrationBuilder.RenameColumn(
                name: "LookDistrictId",
                schema: "master",
                table: "mst_subunit",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_mst_subunit_LookDistrictId",
                schema: "master",
                table: "mst_subunit",
                newName: "IX_mst_subunit_DistrictId");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                schema: "hrm",
                table: "employee_document",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "AccountType",
                schema: "hrm",
                table: "employee_bank_account",
                newName: "BankId");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherAllowance",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                schema: "hrm",
                table: "employee_payroll",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankId",
                schema: "hrm",
                table: "employee_payroll",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                schema: "hrm",
                table: "employee_payroll",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentTypeId",
                schema: "hrm",
                table: "employee_document",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MobileBankingFlag",
                schema: "hrm",
                table: "employee_bank_account",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "bank",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    BankCode = table.Column<string>(type: "text", nullable: false),
                    BankName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RoutingNo = table.Column<string>(type: "text", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeHolidayId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employment_holiday_calendar_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeHolidayId",
                principalSchema: "leave_mgmt",
                principalTable: "holiday_calendar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_subunit_district_DistrictId",
                schema: "master",
                table: "mst_subunit",
                column: "DistrictId",
                principalSchema: "lookup",
                principalTable: "district",
                principalColumn: "Id");
        }
    }
}
