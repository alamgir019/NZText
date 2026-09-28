using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusToPayrollAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "payroll",
                table: "payroll_adjustment");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "payroll",
                table: "payroll_adjustment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "payroll_adjustment_history",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollAdjustmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    PerformedBy = table.Column<string>(type: "text", nullable: true),
                    PerformedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    OldData = table.Column<string>(type: "text", nullable: true),
                    NewData = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_adjustment_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payroll_adjustment_history_payroll_adjustment_PayrollAdjust~",
                        column: x => x.PayrollAdjustmentId,
                        principalSchema: "payroll",
                        principalTable: "payroll_adjustment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payroll_adjustment_history_PayrollAdjustmentId",
                schema: "payroll",
                table: "payroll_adjustment_history",
                column: "PayrollAdjustmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payroll_adjustment_history",
                schema: "payroll");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "payroll",
                table: "payroll_adjustment");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "payroll",
                table: "payroll_adjustment",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
