using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBanglaProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThanaNameBangla",
                schema: "lookup",
                table: "thana",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNameBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuardianNameBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuardianType",
                schema: "hrm",
                table: "employee_personal",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherNameBangla",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentDistrictId",
                schema: "hrm",
                table: "employee_personal",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentDivisionId",
                schema: "hrm",
                table: "employee_personal",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentPostOffice",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentThanaId",
                schema: "hrm",
                table: "employee_personal",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermanentVillageAreaRoad",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentDistrictId",
                schema: "hrm",
                table: "employee_personal",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentDivisionId",
                schema: "hrm",
                table: "employee_personal",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentPostOffice",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentThanaId",
                schema: "hrm",
                table: "employee_personal",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentVillageAreaRoad",
                schema: "hrm",
                table: "employee_personal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionNameBangla",
                schema: "lookup",
                table: "division",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictNameBangla",
                schema: "lookup",
                table: "district",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_personal_PermanentDistrictId",
                schema: "hrm",
                table: "employee_personal",
                column: "PermanentDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_personal_PermanentDivisionId",
                schema: "hrm",
                table: "employee_personal",
                column: "PermanentDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_personal_PermanentThanaId",
                schema: "hrm",
                table: "employee_personal",
                column: "PermanentThanaId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_personal_PresentDistrictId",
                schema: "hrm",
                table: "employee_personal",
                column: "PresentDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_personal_PresentDivisionId",
                schema: "hrm",
                table: "employee_personal",
                column: "PresentDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_personal_PresentThanaId",
                schema: "hrm",
                table: "employee_personal",
                column: "PresentThanaId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_personal_district_PermanentDistrictId",
                schema: "hrm",
                table: "employee_personal",
                column: "PermanentDistrictId",
                principalSchema: "lookup",
                principalTable: "district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_personal_district_PresentDistrictId",
                schema: "hrm",
                table: "employee_personal",
                column: "PresentDistrictId",
                principalSchema: "lookup",
                principalTable: "district",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_personal_division_PermanentDivisionId",
                schema: "hrm",
                table: "employee_personal",
                column: "PermanentDivisionId",
                principalSchema: "lookup",
                principalTable: "division",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_personal_division_PresentDivisionId",
                schema: "hrm",
                table: "employee_personal",
                column: "PresentDivisionId",
                principalSchema: "lookup",
                principalTable: "division",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_personal_thana_PermanentThanaId",
                schema: "hrm",
                table: "employee_personal",
                column: "PermanentThanaId",
                principalSchema: "lookup",
                principalTable: "thana",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_personal_thana_PresentThanaId",
                schema: "hrm",
                table: "employee_personal",
                column: "PresentThanaId",
                principalSchema: "lookup",
                principalTable: "thana",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_personal_district_PermanentDistrictId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_personal_district_PresentDistrictId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_personal_division_PermanentDivisionId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_personal_division_PresentDivisionId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_personal_thana_PermanentThanaId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_personal_thana_PresentThanaId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropIndex(
                name: "IX_employee_personal_PermanentDistrictId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropIndex(
                name: "IX_employee_personal_PermanentDivisionId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropIndex(
                name: "IX_employee_personal_PermanentThanaId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropIndex(
                name: "IX_employee_personal_PresentDistrictId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropIndex(
                name: "IX_employee_personal_PresentDivisionId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropIndex(
                name: "IX_employee_personal_PresentThanaId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "ThanaNameBangla",
                schema: "lookup",
                table: "thana");

            migrationBuilder.DropColumn(
                name: "FatherNameBangla",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "GuardianNameBangla",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "GuardianType",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "MotherNameBangla",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PermanentDistrictId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PermanentDivisionId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PermanentPostOffice",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PermanentThanaId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PermanentVillageAreaRoad",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PresentDistrictId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PresentDivisionId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PresentPostOffice",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PresentThanaId",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "PresentVillageAreaRoad",
                schema: "hrm",
                table: "employee_personal");

            migrationBuilder.DropColumn(
                name: "DivisionNameBangla",
                schema: "lookup",
                table: "division");

            migrationBuilder.DropColumn(
                name: "DistrictNameBangla",
                schema: "lookup",
                table: "district");
        }
    }
}
