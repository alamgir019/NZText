using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_employment_mst_employee_category_EmployeeCategoryId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropTable(
                name: "mst_employee_category",
                schema: "master");

            migrationBuilder.DropIndex(
                name: "IX_employee_employment_EmployeeCategoryId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.DropColumn(
                name: "ReferencePersonId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "Relationship",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "EmployeeCategoryId",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.AlterColumn<string>(
                name: "Relationship",
                schema: "hrm",
                table: "employee_nominee",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NomineeName",
                schema: "hrm",
                table: "employee_nominee",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "NomineeNameBangla",
                schema: "hrm",
                table: "employee_nominee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelationshipBangla",
                schema: "hrm",
                table: "employee_nominee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategory",
                schema: "hrm",
                table: "employee_employment",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomineeNameBangla",
                schema: "hrm",
                table: "employee_nominee");

            migrationBuilder.DropColumn(
                name: "RelationshipBangla",
                schema: "hrm",
                table: "employee_nominee");

            migrationBuilder.DropColumn(
                name: "EmployeeCategory",
                schema: "hrm",
                table: "employee_employment");

            migrationBuilder.AddColumn<string>(
                name: "ReferencePersonId",
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
                name: "Relationship",
                schema: "hrm",
                table: "employee_nominee",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomineeName",
                schema: "hrm",
                table: "employee_nominee",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCategoryId",
                schema: "hrm",
                table: "employee_employment",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mst_employee_category",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CategoryCode = table.Column<string>(type: "text", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OtEligible = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_employee_category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_EmployeeCategoryId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_employment_mst_employee_category_EmployeeCategoryId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeCategoryId",
                principalSchema: "master",
                principalTable: "mst_employee_category",
                principalColumn: "Id");
        }
    }
}
