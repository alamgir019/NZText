using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFromLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "leave_mgmt",
                table: "leave_type");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                schema: "leave_mgmt",
                table: "leave_balance");

            migrationBuilder.DropColumn(
                name: "ActionDate",
                schema: "leave_mgmt",
                table: "leave_approval_history");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "leave_mgmt",
                table: "holiday_calendar");

            migrationBuilder.AlterColumn<string>(
                name: "LeaveYear",
                schema: "leave_mgmt",
                table: "leave_opening_balance",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "YearId",
                schema: "leave_mgmt",
                table: "leave_balance",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "leave_mgmt",
                table: "leave_type",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "LeaveYear",
                schema: "leave_mgmt",
                table: "leave_opening_balance",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "YearId",
                schema: "leave_mgmt",
                table: "leave_balance",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                schema: "leave_mgmt",
                table: "leave_balance",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                schema: "leave_mgmt",
                table: "leave_approval_history",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "leave_mgmt",
                table: "holiday_calendar",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
