using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "employee_transfer",
				schema: "hrm",
				columns: table => new
				{
					Id = table.Column<string>(type: "CHAR(26)", nullable: false),
					EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
					PreviousDepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
					PreviousSectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
					NewDepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
					NewSectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
					EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
					Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
					CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
					CreatedBy = table.Column<string>(type: "text", nullable: false),
					UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
					UpdatedBy = table.Column<string>(type: "text", nullable: false),
					IsActive = table.Column<bool>(type: "boolean", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_employee_transfer", x => x.Id);
					table.ForeignKey(
						name: "FK_employee_transfer_employee_master_EmployeeId",
						column: x => x.EmployeeId,
						principalSchema: "hrm",
						principalTable: "employee_master",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_employee_transfer_mst_department_NewDepartmentId",
						column: x => x.NewDepartmentId,
						principalSchema: "master",
						principalTable: "mst_department",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_employee_transfer_mst_department_PreviousDepartmentId",
						column: x => x.PreviousDepartmentId,
						principalSchema: "master",
						principalTable: "mst_department",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_employee_transfer_mst_section_NewSectionId",
						column: x => x.NewSectionId,
						principalSchema: "master",
						principalTable: "mst_section",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_employee_transfer_mst_section_PreviousSectionId",
						column: x => x.PreviousSectionId,
						principalSchema: "master",
						principalTable: "mst_section",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});
				migrationBuilder.CreateIndex(
					name: "IX_employee_transfer_EmployeeId_EffectiveFrom_IsActive",
					schema: "hrm",
					table: "employee_transfer",
					columns: new[] { "EmployeeId", "EffectiveFrom", "IsActive" });

				migrationBuilder.CreateIndex(
					name: "IX_employee_transfer_NewDepartmentId",
					schema: "hrm",
					table: "employee_transfer",
					column: "NewDepartmentId");

				migrationBuilder.CreateIndex(
					name: "IX_employee_transfer_NewSectionId",
					schema: "hrm",
					table: "employee_transfer",
					column: "NewSectionId");

				migrationBuilder.CreateIndex(
					name: "IX_employee_transfer_PreviousDepartmentId",
					schema: "hrm",
					table: "employee_transfer",
					column: "PreviousDepartmentId");

				migrationBuilder.CreateIndex(
					name: "IX_employee_transfer_PreviousSectionId",
					schema: "hrm",
					table: "employee_transfer",
					column: "PreviousSectionId");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
				migrationBuilder.DropTable(
				   name: "employee_transfer",
				   schema: "hrm");
		}
    }
}
