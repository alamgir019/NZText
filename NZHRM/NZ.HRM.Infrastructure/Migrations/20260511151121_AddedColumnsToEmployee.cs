using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Grades_GradeId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "EmployeePersonals");

            migrationBuilder.RenameColumn(
                name: "TinNumber",
                table: "EmployeePersonals",
                newName: "IDNumber");

            migrationBuilder.AlterColumn<int>(
                name: "Religion",
                table: "EmployeePersonals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Nationality",
                table: "EmployeePersonals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "EmployeePersonals",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatus",
                table: "EmployeePersonals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentType",
                table: "EmployeePersonals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "EmployeePersonals",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "EmployeePersonals",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "GuardianName",
                table: "EmployeePersonals",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuardianType",
                table: "EmployeePersonals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdType",
                table: "EmployeePersonals",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "EmployeeMasters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Shift",
                table: "EmployeeMasters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoiningDate",
                table: "EmployeeMasters",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "Holiday",
                table: "EmployeeMasters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(26)");

            migrationBuilder.AlterColumn<string>(
                name: "EnrollmentId",
                table: "EmployeeMasters",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeType",
                table: "EmployeeMasters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeNature",
                table: "EmployeeMasters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "CellId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMasters_CellId",
                table: "EmployeeMasters",
                column: "CellId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Cells_CellId",
                table: "EmployeeMasters",
                column: "CellId",
                principalTable: "Cells",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Grades_GradeId",
                table: "EmployeeMasters",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Cells_CellId",
                table: "EmployeeMasters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMasters_Grades_GradeId",
                table: "EmployeeMasters");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMasters_CellId",
                table: "EmployeeMasters");

            migrationBuilder.DropColumn(
                name: "GuardianName",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "GuardianType",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "IdType",
                table: "EmployeePersonals");

            migrationBuilder.DropColumn(
                name: "CellId",
                table: "EmployeeMasters");

            migrationBuilder.RenameColumn(
                name: "IDNumber",
                table: "EmployeePersonals",
                newName: "TinNumber");

            migrationBuilder.AlterColumn<int>(
                name: "Religion",
                table: "EmployeePersonals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Nationality",
                table: "EmployeePersonals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "EmployeePersonals",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatus",
                table: "EmployeePersonals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentType",
                table: "EmployeePersonals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "EmployeePersonals",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "EmployeePersonals",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCode",
                table: "EmployeePersonals",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "EmployeeMasters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Shift",
                table: "EmployeeMasters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoiningDate",
                table: "EmployeeMasters",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Holiday",
                table: "EmployeeMasters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "EmployeeMasters",
                type: "CHAR(26)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "CHAR(26)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnrollmentId",
                table: "EmployeeMasters",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeType",
                table: "EmployeeMasters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeNature",
                table: "EmployeeMasters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMasters_Grades_GradeId",
                table: "EmployeeMasters",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
