using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDesignaitonToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DesignationId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_DesignationId",
                table: "EmployeeMasters",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Designations_DesignationId",
                table: "EmployeeMasters",
                column: "DesignationId",
                principalTable: "Designations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Designations_DesignationId",
                table: "EmployeeMasters");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMasters_DesignationId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "EmployeeMasters");
        }
    }
}
