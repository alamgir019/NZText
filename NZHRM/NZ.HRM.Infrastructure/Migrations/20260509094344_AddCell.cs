using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Holidays_HolidayId",
                table: "EmployeeMasters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Shifts_ShiftId",
                table: "EmployeeMasters");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMasters_HolidayId",
                table: "EmployeeMasters");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMasters_ShiftId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "HolidayId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "ShiftId",
                table: "EmployeeMasters");

            migrationBuilder.AddColumn<string>(
                name: "PermanentDistrict",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentDivision",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentPostOffice",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentThana",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentVillageAreaRoad",
                table: "EmployeePersonals",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentDistrict",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentDivision",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentPostOffice",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentThana",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentVillageAreaRoad",
                table: "EmployeePersonals",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferencePersonId",
                table: "EmployeePersonals",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Holiday",
                table: "EmployeeMasters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ProposedMonthlySalary",
                table: "EmployeeMasters",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Shift",
                table: "EmployeeMasters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cells",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameBangla = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cells_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeVerifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SecurityClearanceBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SecurityClearanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EnrolledBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EnrolledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BiometricEnrolledBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BiometricEnrolledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeVerifications_EmployeeMasters_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cells_SectionId",
                table: "Cells",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVerifications_EmployeeId",
                table: "EmployeeVerifications",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cells");

            migrationBuilder.DropTable(
                name: "EmployeeVerifications");

            migrationBuilder.DropColumn(
                name: "PermanentDistrict",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PermanentDivision",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PermanentPostOffice",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PermanentThana",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PermanentVillageAreaRoad",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PresentDistrict",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PresentDivision",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PresentPostOffice",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PresentThana",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "PresentVillageAreaRoad",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "ReferencePersonId",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "Holiday",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "ProposedMonthlySalary",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "EmployeeMasters");

            migrationBuilder.AddColumn<string>(
                name: "HolidayId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShiftId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_HolidayId",
                table: "EmployeeMasters",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_ShiftId",
                table: "EmployeeMasters",
                column: "ShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Holidays_HolidayId",
                table: "EmployeeMasters",
                column: "HolidayId",
                principalTable: "Holidays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Shifts_ShiftId",
                table: "EmployeeMasters",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
