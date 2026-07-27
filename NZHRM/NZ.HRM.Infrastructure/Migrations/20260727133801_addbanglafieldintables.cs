using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addbanglafieldintables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SectionNameBangla",
                schema: "master",
                table: "mst_section",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeNameBangla",
                schema: "master",
                table: "mst_grade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseNameBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SectionNameBangla",
                schema: "master",
                table: "mst_section");

            migrationBuilder.DropColumn(
                name: "GradeNameBangla",
                schema: "master",
                table: "mst_grade");

            migrationBuilder.DropColumn(
                name: "SpouseNameBangla",
                schema: "hrm",
                table: "employee_personal");
        }
    }
}
