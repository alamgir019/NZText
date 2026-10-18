using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    public partial class RemoveOtRequestEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop child table first, then parent ot_request
            migrationBuilder.DropTable(
                name: "ot_request_employee",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "ot_request",
                schema: "attendance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate parent table ot_request
            migrationBuilder.CreateTable(
                name: "ot_request",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CurrentShiftId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OtDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    SubmittedBy = table.Column<string>(type: "text", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ot_request", x => x.Id);
                });

            // Recreate child table ot_request_employee
            migrationBuilder.CreateTable(
                name: "ot_request_employee",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    OvertimeRequestId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OtHours = table.Column<TimeSpan>(type: "interval", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ot_request_employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ot_request_employee_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ot_request_employee_ot_request_OvertimeRequestId",
                        column: x => x.OvertimeRequestId,
                        principalSchema: "attendance",
                        principalTable: "ot_request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ot_request_employee_EmployeeId",
                schema: "attendance",
                table: "ot_request_employee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ot_request_employee_OvertimeRequestId",
                schema: "attendance",
                table: "ot_request_employee",
                column: "OvertimeRequestId");
        }
    }
}
