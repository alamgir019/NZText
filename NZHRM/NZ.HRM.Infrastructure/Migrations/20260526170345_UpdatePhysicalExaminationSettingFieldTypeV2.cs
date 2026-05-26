using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhysicalExaminationSettingFieldTypeV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowRemarks",
                table: "PhysicalExaminationSettings");

            migrationBuilder.DropColumn(
                name: "IsBinaryCheck",
                table: "PhysicalExaminationSettings");

            migrationBuilder.AddColumn<int>(
                name: "FieldType",
                table: "PhysicalExaminationSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OptionValuesJson",
                table: "PhysicalExaminationSettings",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldType",
                table: "PhysicalExaminationSettings");

            migrationBuilder.DropColumn(
                name: "OptionValuesJson",
                table: "PhysicalExaminationSettings");

            migrationBuilder.AddColumn<bool>(
                name: "AllowRemarks",
                table: "PhysicalExaminationSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBinaryCheck",
                table: "PhysicalExaminationSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
