using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.Payroll.Infrastructure.Persistence.Migrations;

public partial class AllowMultipleIncrementRequests : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex(
			name: "IX_increment_request_PayIncHistId",
			table: "increment_request",
			schema: "payroll");

		migrationBuilder.CreateIndex(
			name: "IX_increment_request_PayIncHistId",
			table: "increment_request",
			schema: "payroll",
			column: "PayIncHistId");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex(
			name: "IX_increment_request_PayIncHistId",
			table: "increment_request",
			schema: "payroll");

		migrationBuilder.CreateIndex(
			name: "IX_increment_request_PayIncHistId",
			table: "increment_request",
			schema: "payroll",
			column: "PayIncHistId",
			unique: true);
	}
}
