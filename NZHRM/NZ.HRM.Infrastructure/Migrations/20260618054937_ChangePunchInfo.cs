using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePunchInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PunchDateTime",
                schema: "attendance",
                table: "raw_punch");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "attendance",
                table: "raw_punch");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "attendance",
                table: "device_sync_log");

            migrationBuilder.RenameColumn(
                name: "CardNo",
                schema: "attendance",
                table: "raw_punch",
                newName: "PunchType");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "PunchTime",
                schema: "attendance",
                table: "raw_punch",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                schema: "attendance",
                table: "raw_punch",
                type: "CHAR(26)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCode",
                schema: "attendance",
                table: "raw_punch",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                schema: "master",
                table: "mst_shift",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                schema: "master",
                table: "mst_shift",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.CreateTable(
                name: "processed_punches",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RawPunchId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ShiftId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    PunchDate = table.Column<DateOnly>(type: "date", nullable: false),
                    RawPunchTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    AdjustedPunchTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    PunchType = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processed_punches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_processed_punches_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_processed_punches_mst_shift_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "master",
                        principalTable: "mst_shift",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_processed_punches_raw_punch_RawPunchId",
                        column: x => x.RawPunchId,
                        principalSchema: "attendance",
                        principalTable: "raw_punch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_raw_punch_EmployeeId",
                schema: "attendance",
                table: "raw_punch",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_processed_punches_EmployeeId",
                schema: "attendance",
                table: "processed_punches",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_processed_punches_RawPunchId",
                schema: "attendance",
                table: "processed_punches",
                column: "RawPunchId");

            migrationBuilder.CreateIndex(
                name: "IX_processed_punches_ShiftId",
                schema: "attendance",
                table: "processed_punches",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_raw_punch_employee_master_EmployeeId",
                schema: "attendance",
                table: "raw_punch",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_raw_punch_employee_master_EmployeeId",
                schema: "attendance",
                table: "raw_punch");

            migrationBuilder.DropTable(
                name: "processed_punches",
                schema: "attendance");

            migrationBuilder.DropIndex(
                name: "IX_raw_punch_EmployeeId",
                schema: "attendance",
                table: "raw_punch");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                schema: "attendance",
                table: "raw_punch");

            migrationBuilder.RenameColumn(
                name: "PunchType",
                schema: "attendance",
                table: "raw_punch",
                newName: "CardNo");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PunchTime",
                schema: "attendance",
                table: "raw_punch",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                schema: "attendance",
                table: "raw_punch",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(26)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PunchDateTime",
                schema: "attendance",
                table: "raw_punch",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "attendance",
                table: "raw_punch",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                schema: "master",
                table: "mst_shift",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                schema: "master",
                table: "mst_shift",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "attendance",
                table: "device_sync_log",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
