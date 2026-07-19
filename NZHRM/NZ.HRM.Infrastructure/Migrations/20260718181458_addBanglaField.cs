using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addBanglaField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubunitNameBangla",
                schema: "master",
                table: "mst_subunit",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentNameBangla",
                schema: "master",
                table: "mst_department",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeReferenceBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentPostOfficeBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentVillageAreaRoadBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentPostOfficeBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentVillageAreaRoadBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubunitNameBangla",
                schema: "master",
                table: "mst_subunit");

            migrationBuilder.DropColumn(
                name: "DepartmentNameBangla",
                schema: "master",
                table: "mst_department");

            migrationBuilder.DropColumn(
                name: "EmployeeReferenceBangla",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PermanentPostOfficeBangla",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PermanentVillageAreaRoadBangla",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PresentPostOfficeBangla",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PresentVillageAreaRoadBangla",
                schema: "hrm",
                table: "employee_personal");
        }
    }
}
