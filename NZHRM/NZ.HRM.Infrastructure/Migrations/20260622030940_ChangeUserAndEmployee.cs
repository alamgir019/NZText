using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserAndEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LoginId",
                schema: "security",
                table: "user_account",
                newName: "Role");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompliant",
                schema: "master",
                table: "mst_unit",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "SecurityClearanceDate",
                schema: "hrm",
                table: "employee_verification",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EnrolledDate",
                schema: "hrm",
                table: "employee_verification",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BiometricEnrolledDate",
                schema: "hrm",
                table: "employee_verification",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "hrm",
                table: "employee_master",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompliant",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "hrm",
                table: "employee_master");

            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "security",
                table: "user_account",
                newName: "LoginId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SecurityClearanceDate",
                schema: "hrm",
                table: "employee_verification",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrolledDate",
                schema: "hrm",
                table: "employee_verification",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BiometricEnrolledDate",
                schema: "hrm",
                table: "employee_verification",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
