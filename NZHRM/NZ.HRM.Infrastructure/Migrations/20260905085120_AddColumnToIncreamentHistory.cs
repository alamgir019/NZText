using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToIncreamentHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
