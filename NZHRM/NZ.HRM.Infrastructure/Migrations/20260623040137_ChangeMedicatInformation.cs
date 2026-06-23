using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMedicatInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_medical_fitness_check_EmployeeId",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.CreateIndex(
                name: "IX_medical_fitness_check_EmployeeId",
                schema: "hrm",
                table: "medical_fitness_check",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_medical_fitness_check_EmployeeId",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.CreateIndex(
                name: "IX_medical_fitness_check_EmployeeId",
                schema: "hrm",
                table: "medical_fitness_check",
                column: "EmployeeId");
        }
    }
}
