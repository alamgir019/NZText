using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitToOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                schema: "attendance",
                table: "ot_request_item",
                type: "CHAR(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                schema: "attendance",
                table: "ot_request_item",
                type: "CHAR(26)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                schema: "attendance",
                table: "ot_request_item",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ot_request_item_DepartmentId",
                schema: "attendance",
                table: "ot_request_item",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ot_request_item_EmployeeId",
                schema: "attendance",
                table: "ot_request_item",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ot_request_item_UnitId",
                schema: "attendance",
                table: "ot_request_item",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ot_request_item_employee_master_EmployeeId",
                schema: "attendance",
                table: "ot_request_item",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ot_request_item_mst_department_DepartmentId",
                schema: "attendance",
                table: "ot_request_item",
                column: "DepartmentId",
                principalSchema: "master",
                principalTable: "mst_department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ot_request_item_mst_unit_UnitId",
                schema: "attendance",
                table: "ot_request_item",
                column: "UnitId",
                principalSchema: "master",
                principalTable: "mst_unit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ot_request_item_employee_master_EmployeeId",
                schema: "attendance",
                table: "ot_request_item");

            migrationBuilder.DropForeignKey(
                name: "FK_ot_request_item_mst_department_DepartmentId",
                schema: "attendance",
                table: "ot_request_item");

            migrationBuilder.DropForeignKey(
                name: "FK_ot_request_item_mst_unit_UnitId",
                schema: "attendance",
                table: "ot_request_item");

            migrationBuilder.DropIndex(
                name: "IX_ot_request_item_DepartmentId",
                schema: "attendance",
                table: "ot_request_item");

            migrationBuilder.DropIndex(
                name: "IX_ot_request_item_EmployeeId",
                schema: "attendance",
                table: "ot_request_item");

            migrationBuilder.DropIndex(
                name: "IX_ot_request_item_UnitId",
                schema: "attendance",
                table: "ot_request_item");

            migrationBuilder.DropColumn(
                name: "UnitId",
                schema: "attendance",
                table: "ot_request_item");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                schema: "attendance",
                table: "ot_request_item",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(26)");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                schema: "attendance",
                table: "ot_request_item",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(26)");
        }
    }
}
