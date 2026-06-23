using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMedialFitness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medical_fitness_check_blood_group_BloodGroupId",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.DropTable(
                name: "blood_group",
                schema: "lookup");

            migrationBuilder.DropIndex(
                name: "IX_medical_fitness_check_BloodGroupId",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.DropColumn(
                name: "BloodGroupId",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.DropColumn(
                name: "HeightCm",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.DropColumn(
                name: "IsFit",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.DropColumn(
                name: "PhysicalExaminationDataJson",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.DropColumn(
                name: "WeightKg",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fitness",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationSign",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fitness",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.DropColumn(
                name: "IdentificationSign",
                schema: "hrm",
                table: "medical_fitness_check");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodGroupId",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HeightCm",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFit",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalExaminationDataJson",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightKg",
                schema: "hrm",
                table: "medical_fitness_check",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "blood_group",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    BloodGroupCode = table.Column<string>(type: "text", nullable: false),
                    BloodGroupName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blood_group", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medical_fitness_check_BloodGroupId",
                schema: "hrm",
                table: "medical_fitness_check",
                column: "BloodGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_medical_fitness_check_blood_group_BloodGroupId",
                schema: "hrm",
                table: "medical_fitness_check",
                column: "BloodGroupId",
                principalSchema: "lookup",
                principalTable: "blood_group",
                principalColumn: "Id");
        }
    }
}
