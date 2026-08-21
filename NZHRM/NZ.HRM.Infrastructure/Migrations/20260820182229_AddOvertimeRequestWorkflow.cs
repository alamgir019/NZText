using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOvertimeRequestWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                schema: "attendance",
                table: "ot_request",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                schema: "attendance",
                table: "ot_request",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubmittedBy",
                schema: "attendance",
                table: "ot_request",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedOn",
                schema: "attendance",
                table: "ot_request",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                schema: "attendance",
                table: "ot_request");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "attendance",
                table: "ot_request");

            migrationBuilder.DropColumn(
                name: "SubmittedBy",
                schema: "attendance",
                table: "ot_request");

            migrationBuilder.DropColumn(
                name: "SubmittedOn",
                schema: "attendance",
                table: "ot_request");
        }
    }
}
