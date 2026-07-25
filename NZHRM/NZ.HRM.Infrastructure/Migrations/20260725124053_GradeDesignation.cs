using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GradeDesignation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEnglish",
                schema: "master",
                table: "mst_cell");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNature",
                schema: "master",
                table: "mst_grade",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeId",
                schema: "master",
                table: "mst_designation",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mst_designation_GradeId",
                schema: "master",
                table: "mst_designation",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_designation_mst_grade_GradeId",
                schema: "master",
                table: "mst_designation",
                column: "GradeId",
                principalSchema: "master",
                principalTable: "mst_grade",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mst_designation_mst_grade_GradeId",
                schema: "master",
                table: "mst_designation");

            migrationBuilder.DropIndex(
                name: "IX_mst_designation_GradeId",
                schema: "master",
                table: "mst_designation");

            migrationBuilder.DropColumn(
                name: "EmployeeNature",
                schema: "master",
                table: "mst_grade");

            migrationBuilder.DropColumn(
                name: "GradeId",
                schema: "master",
                table: "mst_designation");

            migrationBuilder.AddColumn<string>(
                name: "NameEnglish",
                schema: "master",
                table: "mst_cell",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
