using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCompliant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Locations_LocationId1",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LocationId1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Designations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "Companies",
                type: "CHAR(26)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompliant",
                table: "Companies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationId",
                table: "Companies",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Locations_LocationId",
                table: "Companies",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Locations_LocationId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LocationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "IsCompliant",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Companies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "CHAR(26)");

            migrationBuilder.AddColumn<string>(
                name: "LocationId1",
                table: "Companies",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LocationId1",
                table: "Companies",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Locations_LocationId1",
                table: "Companies",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
