using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPayrollAllowance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "holiday_calendar",
                schema: "attendance");

            migrationBuilder.DropColumn(
                name: "EmployeeType",
                schema: "master",
                table: "mst_grade");

            migrationBuilder.RenameColumn(
                name: "PresentAddress",
                schema: "hrm",
                table: "employee_contact",
                newName: "PresentVillage");

            migrationBuilder.RenameColumn(
                name: "PermanentAddress",
                schema: "hrm",
                table: "employee_contact",
                newName: "PresentUpazilaId");

            migrationBuilder.AddColumn<string>(
                name: "MstGroupComplexId",
                schema: "master",
                table: "mst_unit",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasicSalary",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConveyanceAllowance",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FoodAllowance",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HouseRentAllowance",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MedicalAllowance",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherAllowance",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                schema: "hrm",
                table: "employee_payroll",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TINNo",
                schema: "hrm",
                table: "employee_payroll",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                schema: "hrm",
                table: "employee_payroll",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeType",
                schema: "hrm",
                table: "employee_master",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentDistrictId",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentDivisionId",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentPostOffice",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentUpazilaId",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentVillage",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentDistrictId",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentDivisionId",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentPostOffice",
                schema: "hrm",
                table: "employee_contact",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mst_group_complex",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    GroupCode = table.Column<string>(type: "text", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    MstGroupId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_group_complex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_group_complex_mst_group_MstGroupId",
                        column: x => x.MstGroupId,
                        principalSchema: "master",
                        principalTable: "mst_group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_mst_unit_MstGroupComplexId",
                schema: "master",
                table: "mst_unit",
                column: "MstGroupComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeNatureId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_group_complex_MstGroupId",
                schema: "master",
                table: "mst_group_complex",
                column: "MstGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employment_employee_nature_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeNatureId",
                principalSchema: "lookup",
                principalTable: "employee_nature",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employment_holiday_calendar_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeHolidayId",
                principalSchema: "leave_mgmt",
                principalTable: "holiday_calendar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_unit_mst_group_complex_MstGroupComplexId",
                schema: "master",
                table: "mst_unit",
                column: "MstGroupComplexId",
                principalSchema: "master",
                principalTable: "mst_group_complex",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employment_employee_nature_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_employment_holiday_calendar_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropForeignKey(
                name: "FK_mst_unit_mst_group_complex_MstGroupComplexId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropTable(
                name: "mst_group_complex",
                schema: "master");

            migrationBuilder.DropIndex(
                name: "IX_mst_unit_MstGroupComplexId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropIndex(
                name: "IX_employee_employment_EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropIndex(
                name: "IX_employee_employment_EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "MstGroupComplexId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "ConveyanceAllowance",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "FoodAllowance",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "HouseRentAllowance",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "MedicalAllowance",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "OtherAllowance",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "TINNo",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "Tax",
                schema: "hrm",
                table: "employee_payroll");

            migrationBuilder.DropColumn(
                name: "EmployeeType",
                schema: "hrm",
                table: "employee_master");

            migrationBuilder.DropColumn(
                name: "EmployeeHolidayId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "EmployeeNatureId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "PermanentDistrictId",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.DropColumn(
                name: "PermanentDivisionId",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.DropColumn(
                name: "PermanentPostOffice",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.DropColumn(
                name: "PermanentUpazilaId",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.DropColumn(
                name: "PermanentVillage",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.DropColumn(
                name: "PresentDistrictId",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.DropColumn(
                name: "PresentDivisionId",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.DropColumn(
                name: "PresentPostOffice",
                schema: "hrm",
                table: "employee_contact");

            migrationBuilder.RenameColumn(
                name: "PresentVillage",
                schema: "hrm",
                table: "employee_contact",
                newName: "PresentAddress");

            migrationBuilder.RenameColumn(
                name: "PresentUpazilaId",
                schema: "hrm",
                table: "employee_contact",
                newName: "PermanentAddress");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeType",
                schema: "master",
                table: "mst_grade",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "holiday_calendar",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    HolidayDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HolidayName = table.Column<string>(type: "text", nullable: false),
                    HolidayType = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    UnitId = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_holiday_calendar", x => x.Id);
                });
        }
    }
}
