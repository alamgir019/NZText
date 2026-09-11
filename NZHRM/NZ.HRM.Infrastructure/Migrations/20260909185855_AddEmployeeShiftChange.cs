using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeShiftChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee_shift_change",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PreviousShiftId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    NewShiftId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_shift_change", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_shift_change_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employee_shift_change_mst_shift_NewShiftId",
                        column: x => x.NewShiftId,
                        principalSchema: "master",
                        principalTable: "mst_shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employee_shift_change_mst_shift_PreviousShiftId",
                        column: x => x.PreviousShiftId,
                        principalSchema: "master",
                        principalTable: "mst_shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_shift_change_EmployeeId_EffectiveFrom_IsActive",
                schema: "hrm",
                table: "employee_shift_change",
                columns: new[] { "EmployeeId", "EffectiveFrom", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_employee_shift_change_NewShiftId",
                schema: "hrm",
                table: "employee_shift_change",
                column: "NewShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_shift_change_PreviousShiftId",
                schema: "hrm",
                table: "employee_shift_change",
                column: "PreviousShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_shift_change",
                schema: "hrm");
        }
    }
}
