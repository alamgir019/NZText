using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnLeaveEncash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "FromDate",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instalment",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ToDate",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "Instalment",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "ToDate",
                schema: "leave_mgmt",
                table: "leave_encashment");
        }
    }
}
