using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CurrectSecurityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "user_session");

            migrationBuilder.DropColumn(
                name: "ActiveFlag",
                schema: "security",
                table: "user_role");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "user_role");

            migrationBuilder.DropColumn(
                name: "ActiveFlag",
                schema: "security",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "security",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "MobileNo",
                schema: "security",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "security",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "user_account");

            migrationBuilder.DropColumn(
                name: "ActiveFlag",
                schema: "security",
                table: "role_permission");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "role_permission");

            migrationBuilder.DropColumn(
                name: "ActiveFlag",
                schema: "security",
                table: "role");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "role");

            migrationBuilder.DropColumn(
                name: "ActiveFlag",
                schema: "security",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "permission");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "password_history");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "module_access");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "field_security");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "security",
                table: "emergency_access");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                schema: "security",
                table: "user_account",
                type: "CHAR(26)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PermissionType",
                schema: "security",
                table: "permission",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_account_EmployeeId",
                schema: "security",
                table: "user_account",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_account_employee_master_EmployeeId",
                schema: "security",
                table: "user_account",
                column: "EmployeeId",
                principalSchema: "hrm",
                principalTable: "employee_master",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_account_employee_master_EmployeeId",
                schema: "security",
                table: "user_account");

            migrationBuilder.DropIndex(
                name: "IX_user_account_EmployeeId",
                schema: "security",
                table: "user_account");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "user_session",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveFlag",
                schema: "security",
                table: "user_role",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "user_role",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                schema: "security",
                table: "user_account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(26)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveFlag",
                schema: "security",
                table: "user_account",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "security",
                table: "user_account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNo",
                schema: "security",
                table: "user_account",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "security",
                table: "user_account",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "user_account",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveFlag",
                schema: "security",
                table: "role_permission",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "role_permission",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveFlag",
                schema: "security",
                table: "role",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "role",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PermissionType",
                schema: "security",
                table: "permission",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveFlag",
                schema: "security",
                table: "permission",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "permission",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "password_history",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "module_access",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "field_security",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "security",
                table: "emergency_access",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
