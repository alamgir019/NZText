using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftNature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shift",
                table: "EmployeeMasters");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeType",
                table: "Grades",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShiftId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocationDepartments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LocationId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationDepartments_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationDepartments_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_ShiftId",
                table: "EmployeeMasters",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationDepartments_DepartmentId",
                table: "LocationDepartments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationDepartments_LocationId",
                table: "LocationDepartments",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Shifts_ShiftId",
                table: "EmployeeMasters",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Shifts_ShiftId",
                table: "EmployeeMasters");

            migrationBuilder.DropTable(
                name: "LocationDepartments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMasters_ShiftId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "EmployeeType",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "EmployeeMasters");

            migrationBuilder.AddColumn<int>(
                name: "Shift",
                table: "EmployeeMasters",
                type: "integer",
                nullable: true);
        }
    }
}
