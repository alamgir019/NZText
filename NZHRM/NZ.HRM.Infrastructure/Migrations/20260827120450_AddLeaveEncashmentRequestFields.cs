using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveEncashmentRequestFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForwardedBy",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ForwardedDate",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForwardedBy",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "ForwardedDate",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "Reason",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "leave_mgmt",
                table: "leave_encashment");
        }
    }
}
