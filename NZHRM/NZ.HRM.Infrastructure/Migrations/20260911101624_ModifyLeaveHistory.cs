using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyLeaveHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "ForwardedBy",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "ForwardedDate",
                schema: "leave_mgmt",
                table: "leave_encashment");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "leave_mgmt",
                table: "leave_approval_history");

            migrationBuilder.DropColumn(
                name: "ForwardedBy",
                schema: "leave_mgmt",
                table: "leave_application");

            migrationBuilder.DropColumn(
                name: "ForwardedDate",
                schema: "leave_mgmt",
                table: "leave_application");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                schema: "attendance",
                table: "attendance_exception",
                type: "time without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "leave_encashment_history",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EncashmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowStepNo = table.Column<int>(type: "integer", nullable: false),
                    ApproverId = table.Column<string>(type: "text", nullable: true),
                    ActionTaken = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_encashment_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_encashment_history_leave_encashment_EncashmentId",
                        column: x => x.EncashmentId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_encashment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_leave_encashment_history_EncashmentId",
                schema: "leave_mgmt",
                table: "leave_encashment_history",
                column: "EncashmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leave_encashment_history",
                schema: "leave_mgmt");

            migrationBuilder.DropColumn(
                name: "Time",
                schema: "attendance",
                table: "attendance_exception");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                schema: "leave_mgmt",
                table: "leave_encashment",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "leave_mgmt",
                table: "leave_approval_history",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
    }
}
