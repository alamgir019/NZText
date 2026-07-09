using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_contact",
                schema: "hrm");

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.CreateTable(
                name: "employee_contact",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    EmergencyContactNo = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    PermanentDistrictId = table.Column<string>(type: "text", nullable: true),
                    PermanentDivisionId = table.Column<string>(type: "text", nullable: true),
                    PermanentPostOffice = table.Column<string>(type: "text", nullable: true),
                    PermanentUpazilaId = table.Column<string>(type: "text", nullable: true),
                    PermanentVillage = table.Column<string>(type: "text", nullable: true),
                    PersonalEmail = table.Column<string>(type: "text", nullable: true),
                    PresentDistrictId = table.Column<string>(type: "text", nullable: true),
                    PresentDivisionId = table.Column<string>(type: "text", nullable: true),
                    PresentPostOffice = table.Column<string>(type: "text", nullable: true),
                    PresentUpazilaId = table.Column<string>(type: "text", nullable: true),
                    PresentVillage = table.Column<string>(type: "text", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_contact_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_contact_EmployeeId",
                schema: "hrm",
                table: "employee_contact",
                column: "EmployeeId",
                unique: true);
        }
    }
}
