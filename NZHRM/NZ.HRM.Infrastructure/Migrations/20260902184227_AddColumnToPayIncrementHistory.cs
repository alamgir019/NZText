using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToPayIncrementHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.AddColumn<DateTime>(
                name: "ForwardDate",
                schema: "payroll",
                table: "increment_history",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForwardedBy",
                schema: "payroll",
                table: "increment_history",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncrementType",
                schema: "payroll",
                table: "increment_history",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.DropColumn(
                name: "ForwardDate",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.DropColumn(
                name: "ForwardedBy",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.DropColumn(
                name: "IncrementType",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.AddForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
