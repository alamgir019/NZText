using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromStatus",
                schema: "payroll",
                table: "increment_request",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToStatus",
                schema: "payroll",
                table: "increment_request",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromStatus",
                schema: "payroll",
                table: "increment_request");

            migrationBuilder.DropColumn(
                name: "ToStatus",
                schema: "payroll",
                table: "increment_request");
        }
    }
}
