using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "attendance",
                table: "processed_attendance");

            migrationBuilder.AddColumn<int>(
                name: "DaysOffset",
                schema: "master",
                table: "mst_shift",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShiftType",
                schema: "master",
                table: "mst_shift",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOffset",
                schema: "master",
                table: "mst_shift");

            migrationBuilder.DropColumn(
                name: "ShiftType",
                schema: "master",
                table: "mst_shift");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "attendance",
                table: "processed_attendance",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
