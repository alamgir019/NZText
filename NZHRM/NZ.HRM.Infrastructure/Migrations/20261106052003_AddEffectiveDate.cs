using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEffectiveDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PreviousShiftId",
                schema: "hrm",
                table: "employee_shift_change",
                type: "CHAR(26)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(26)");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EffectiveTo",
                schema: "hrm",
                table: "employee_shift_change",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_employee_shift_change_EmployeeId_EffectiveFrom_EffectiveTo_~",
                schema: "hrm",
                table: "employee_shift_change",
                columns: new[] { "EmployeeId", "EffectiveFrom", "EffectiveTo", "IsActive" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_employee_shift_change_EmployeeId_EffectiveFrom_EffectiveTo_~",
                schema: "hrm",
                table: "employee_shift_change");

            migrationBuilder.DropColumn(
                name: "EffectiveTo",
                schema: "hrm",
                table: "employee_shift_change");

            migrationBuilder.AlterColumn<string>(
                name: "PreviousShiftId",
                schema: "hrm",
                table: "employee_shift_change",
                type: "CHAR(26)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "CHAR(26)",
                oldNullable: true);
        }
    }
}
