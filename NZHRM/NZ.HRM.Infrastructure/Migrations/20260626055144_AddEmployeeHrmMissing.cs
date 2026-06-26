using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeHrmMissing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mst_department_section",
                schema: "master");

            migrationBuilder.DropTable(
                name: "mst_subunit_department",
                schema: "master");

            migrationBuilder.DropColumn(
                name: "SubunitId",
                schema: "master",
                table: "mst_department");

            migrationBuilder.DropColumn(
                name: "EmployeeNameEnglish",
                schema: "hrm",
                table: "employee_master");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                schema: "master",
                table: "mst_section",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SectionId",
                schema: "master",
                table: "mst_section",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeReference",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceMobileNumber",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferencePersonId",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceType",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OldCardNo",
                schema: "hrm",
                table: "employee_master",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "mst_department_unit_complex",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UnitId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ComplexId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_department_unit_complex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_department_unit_complex_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "master",
                        principalTable: "mst_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mst_department_unit_complex_mst_group_complex_ComplexId",
                        column: x => x.ComplexId,
                        principalSchema: "master",
                        principalTable: "mst_group_complex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mst_department_unit_complex_mst_unit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "master",
                        principalTable: "mst_unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mst_section_SectionId",
                schema: "master",
                table: "mst_section",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_unit_complex_ComplexId",
                schema: "master",
                table: "mst_department_unit_complex",
                column: "ComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_unit_complex_DepartmentId",
                schema: "master",
                table: "mst_department_unit_complex",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_unit_complex_UnitId",
                schema: "master",
                table: "mst_department_unit_complex",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_section_mst_department_SectionId",
                schema: "master",
                table: "mst_section",
                column: "SectionId",
                principalSchema: "master",
                principalTable: "mst_department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mst_section_mst_department_SectionId",
                schema: "master",
                table: "mst_section");

            migrationBuilder.DropTable(
                name: "mst_department_unit_complex",
                schema: "master");

            migrationBuilder.DropIndex(
                name: "IX_mst_section_SectionId",
                schema: "master",
                table: "mst_section");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "master",
                table: "mst_section");

            migrationBuilder.DropColumn(
                name: "SectionId",
                schema: "master",
                table: "mst_section");

            migrationBuilder.DropColumn(
                name: "EmployeeReference",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "ReferenceMobileNumber",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "ReferencePersonId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "ReferenceType",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "Relationship",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.AddColumn<string>(
                name: "SubunitId",
                schema: "master",
                table: "mst_department",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OldCardNo",
                schema: "hrm",
                table: "employee_master",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNameEnglish",
                schema: "hrm",
                table: "employee_master",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "mst_department_section",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_department_section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_department_section_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "master",
                        principalTable: "mst_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mst_department_section_mst_section_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "master",
                        principalTable: "mst_section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mst_subunit_department",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SubunitId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_subunit_department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_subunit_department_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "master",
                        principalTable: "mst_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mst_subunit_department_mst_subunit_SubunitId",
                        column: x => x.SubunitId,
                        principalSchema: "master",
                        principalTable: "mst_subunit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_section_DepartmentId",
                schema: "master",
                table: "mst_department_section",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_section_SectionId",
                schema: "master",
                table: "mst_department_section",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_subunit_department_DepartmentId",
                schema: "master",
                table: "mst_subunit_department",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_subunit_department_SubunitId",
                schema: "master",
                table: "mst_subunit_department",
                column: "SubunitId");
        }
    }
}
