using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyPayIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
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

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "payroll",
                table: "increment_history",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "increment_request",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayIncHistId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_increment_request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_increment_request_increment_history_PayIncHistId",
                        column: x => x.PayIncHistId,
                        principalSchema: "payroll",
                        principalTable: "increment_history",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_increment_request_PayIncHistId",
                schema: "payroll",
                table: "increment_request",
                column: "PayIncHistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "increment_request",
                schema: "payroll");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                schema: "payroll",
                table: "increment_history",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                schema: "payroll",
                table: "increment_history",
                type: "text",
                nullable: true);

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
        }
    }
}
