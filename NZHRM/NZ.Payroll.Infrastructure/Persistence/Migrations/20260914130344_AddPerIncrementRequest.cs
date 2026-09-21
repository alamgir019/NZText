using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.Payroll.Infrastructure.Persistence.Migrations;

public partial class AddPerIncrementRequest : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "payroll");

		migrationBuilder.CreateTable(
			name: "increment_request",
			schema: "payroll",
			columns: table => new
			{
				Id = table.Column<string>(type: "CHAR(26)", nullable: false),
				PayIncHistId = table.Column<string>(type: "CHAR(26)", nullable: false),
				ApprovedBy = table.Column<string>(type: "text", nullable: true),
				ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
				CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
				CreatedBy = table.Column<string>(type: "text", nullable: false),
				UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
				UpdatedBy = table.Column<string>(type: "text", nullable: false),
				IsActive = table.Column<bool>(type: "boolean", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_increment_request", x => x.Id);
				table.ForeignKey(
					name: "FK_increment_request_increment_history_PayIncHistId",
					column: x => x.PayIncHistId,
					principalSchema: "payroll",
					principalTable: "increment_history",
					principalColumn: "Id",
					onDelete: ReferentialAction.Restrict);
			});

		migrationBuilder.CreateIndex(
			name: "IX_increment_request_PayIncHistId",
			table: "increment_request",
			schema: "payroll",
			column: "PayIncHistId",
			unique: false);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "increment_request",
			schema: "payroll");
	}
}
