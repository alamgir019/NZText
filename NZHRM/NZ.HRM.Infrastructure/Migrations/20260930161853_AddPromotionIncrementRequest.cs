using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPromotionIncrementRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promotion_increment_request",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CurrentDesignationId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    ProposedDesignationId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CurrentGradeId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    ProposedGradeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CurrentGrossSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IncrementPercent = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    IncrementAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    NewGrossSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CurrentStepNo = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotion_increment_request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_promotion_increment_request_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_promotion_increment_request_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "master",
                        principalTable: "mst_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_promotion_increment_request_mst_designation_CurrentDesignat~",
                        column: x => x.CurrentDesignationId,
                        principalSchema: "master",
                        principalTable: "mst_designation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_promotion_increment_request_mst_designation_ProposedDesigna~",
                        column: x => x.ProposedDesignationId,
                        principalSchema: "master",
                        principalTable: "mst_designation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_promotion_increment_request_mst_grade_CurrentGradeId",
                        column: x => x.CurrentGradeId,
                        principalSchema: "master",
                        principalTable: "mst_grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_promotion_increment_request_mst_grade_ProposedGradeId",
                        column: x => x.ProposedGradeId,
                        principalSchema: "master",
                        principalTable: "mst_grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "promotion_increment_approval_history",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PromotionIncrementRequestId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    StepNo = table.Column<int>(type: "integer", nullable: false),
                    StepName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FromStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ToStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ActionBy = table.Column<string>(type: "text", nullable: false),
                    ActionOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotion_increment_approval_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_promotion_increment_approval_history_promotion_increment_re~",
                        column: x => x.PromotionIncrementRequestId,
                        principalSchema: "payroll",
                        principalTable: "promotion_increment_request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_approval_history_PromotionIncrementRequ~",
                schema: "payroll",
                table: "promotion_increment_approval_history",
                column: "PromotionIncrementRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_CurrentDesignationId",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "CurrentDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_CurrentGradeId",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "CurrentGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_DepartmentId",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_EmployeeId_Status",
                schema: "payroll",
                table: "promotion_increment_request",
                columns: new[] { "EmployeeId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_ProposedDesignationId",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "ProposedDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_ProposedGradeId",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "ProposedGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_Status",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promotion_increment_approval_history",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "promotion_increment_request",
                schema: "payroll");
        }
    }
}
