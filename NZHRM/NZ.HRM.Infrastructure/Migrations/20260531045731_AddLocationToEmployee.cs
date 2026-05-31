using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_LocationId",
                table: "EmployeeMasters",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Locations_LocationId",
                table: "EmployeeMasters",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Locations_LocationId",
                table: "EmployeeMasters");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMasters_LocationId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "EmployeeMasters");
        }
    }
}
