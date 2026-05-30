using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicalFitnessAndOthers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cells_Sections_SectionId",
                table: "Cells");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Departments_DepartmentId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_DepartmentId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Cells_SectionId",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Cells");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Menus",
                newName: "SortOrder");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Thanas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Shifts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Locations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Holidays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Divisions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Districts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Designations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Cells",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DepartmentSections",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentSections_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentSections_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalFitnessChecks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EnrollmentId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BloodGroup = table.Column<int>(type: "integer", maxLength: 20, nullable: true),
                    HeightCm = table.Column<decimal>(type: "numeric", nullable: true),
                    WeightKg = table.Column<decimal>(type: "numeric", nullable: true),
                    PhysicalExaminationDataJson = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsFit = table.Column<bool>(type: "boolean", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ExaminedByDoctor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExaminationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalFitnessChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalFitnessChecks_EmployeeMasters_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExaminationSettings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FieldName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsBinaryCheck = table.Column<bool>(type: "boolean", nullable: false),
                    AllowRemarks = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExaminationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionCells",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CellId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionCells_Cells_CellId",
                        column: x => x.CellId,
                        principalTable: "Cells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionCells_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSections_DepartmentId",
                table: "DepartmentSections",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSections_SectionId",
                table: "DepartmentSections",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalFitnessChecks_EmployeeId",
                table: "MedicalFitnessChecks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionCells_CellId",
                table: "SectionCells",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionCells_SectionId",
                table: "SectionCells",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentSections");

            migrationBuilder.DropTable(
                name: "MedicalFitnessChecks");

            migrationBuilder.DropTable(
                name: "PhysicalExaminationSettings");

            migrationBuilder.DropTable(
                name: "SectionCells");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Thanas");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Divisions");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Cells");

            migrationBuilder.RenameColumn(
                name: "SortOrder",
                table: "Menus",
                newName: "Order");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Sections",
                type: "CHAR(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectionId",
                table: "Cells",
                type: "CHAR(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_DepartmentId",
                table: "Sections",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cells_SectionId",
                table: "Cells",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cells_Sections_SectionId",
                table: "Cells",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Departments_DepartmentId",
                table: "Sections",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
