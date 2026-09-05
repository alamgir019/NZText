using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExceptionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResolvedBy",
                schema: "attendance",
                table: "attendance_exception");

            migrationBuilder.DropColumn(
                name: "ResolvedFlag",
                schema: "attendance",
                table: "attendance_exception");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "attendance",
                table: "attendance_exception",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "attendance_exception_history",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AttendanceExceptionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FromStatus = table.Column<int>(type: "integer", nullable: false),
                    ToStatus = table.Column<int>(type: "integer", nullable: false),
                    ActionBy = table.Column<string>(type: "text", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance_exception_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendance_exception_history_attendance_exception_Attendanc~",
                        column: x => x.AttendanceExceptionId,
                        principalSchema: "attendance",
                        principalTable: "attendance_exception",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendance_exception_history_AttendanceExceptionId",
                schema: "attendance",
                table: "attendance_exception_history",
                column: "AttendanceExceptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendance_exception_history",
                schema: "attendance");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "attendance",
                table: "attendance_exception");

            migrationBuilder.AddColumn<string>(
                name: "ResolvedBy",
                schema: "attendance",
                table: "attendance_exception",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResolvedFlag",
                schema: "attendance",
                table: "attendance_exception",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
