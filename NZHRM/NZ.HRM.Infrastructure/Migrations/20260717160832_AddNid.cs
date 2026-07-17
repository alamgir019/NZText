using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NidNo",
                schema: "hrm",
                table: "employee_personal",
                newName: "IdNumber");

            migrationBuilder.AddColumn<int>(
                name: "IdType",
                schema: "hrm",
                table: "employee_personal",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdType",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.RenameColumn(
                name: "IdNumber",
                schema: "hrm",
                table: "employee_personal",
                newName: "NidNo");
        }
    }
}
