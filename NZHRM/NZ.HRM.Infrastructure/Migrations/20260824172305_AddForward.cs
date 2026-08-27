using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddForward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForwardedBy",
                schema: "leave_mgmt",
                table: "leave_application",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ForwardedDate",
                schema: "leave_mgmt",
                table: "leave_application",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForwardedBy",
                schema: "leave_mgmt",
                table: "leave_application");

            migrationBuilder.DropColumn(
                name: "ForwardedDate",
                schema: "leave_mgmt",
                table: "leave_application");
        }
    }
}
