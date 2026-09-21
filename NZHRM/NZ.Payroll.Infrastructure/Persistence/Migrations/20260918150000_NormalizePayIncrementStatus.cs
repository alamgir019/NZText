using Microsoft.EntityFrameworkCore.Migrations;
using NZ.HRM.Domain.Constants;

#nullable disable

namespace NZ.Payroll.Infrastructure.Persistence.Migrations;

public partial class NormalizePayIncrementStatus : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql($"""
			UPDATE payroll.increment_history
			SET "Status" = '{PayIncrementStatuses.Pending}'
			WHERE "Status" IS NULL OR btrim("Status") = '';
		""");

		migrationBuilder.AlterColumn<string>(
			name: "Status",
			schema: "payroll",
			table: "increment_history",
			type: "text",
			nullable: false,
			defaultValue: PayIncrementStatuses.Pending,
			oldClrType: typeof(string),
			oldType: "text",
			oldNullable: false,
			oldDefaultValue: "");
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "Status",
			schema: "payroll",
			table: "increment_history",
			type: "text",
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "text",
			oldNullable: false,
			oldDefaultValue: PayIncrementStatuses.Pending);
	}
}
