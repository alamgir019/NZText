using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLearnerConfirmationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.CreateTable(
                name: "learner_confirmation_request",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DateOfJoining = table.Column<DateOnly>(type: "date", nullable: false),
                    ProbationPeriodMonths = table.Column<int>(type: "integer", nullable: false),
                    ProbationCompletedOn = table.Column<DateOnly>(type: "date", nullable: false),
                    CurrentGrossSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    StandardGrossSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AdjustmentAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ForwardedBy = table.Column<string>(type: "text", nullable: false),
                    ForwardedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learner_confirmation_request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_learner_confirmation_request_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_learner_confirmation_request_EmployeeId_Status",
                schema: "hrm",
                table: "learner_confirmation_request",
                columns: new[] { "EmployeeId", "Status" });

            migrationBuilder.AddForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history");

            migrationBuilder.DropTable(
                name: "learner_confirmation_request",
                schema: "hrm");

            migrationBuilder.AddForeignKey(
                name: "FK_increment_history_employee_master_EmployeeId",
                schema: "payroll",
                table: "increment_history",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
