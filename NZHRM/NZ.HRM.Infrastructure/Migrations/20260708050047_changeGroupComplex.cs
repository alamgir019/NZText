using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeGroupComplex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mst_designation_mst_grade_GradeId",
                schema: "master",
                table: "mst_designation");

            migrationBuilder.DropForeignKey(
                name: "FK_mst_group_complex_mst_group_MstGroupId",
                schema: "master",
                table: "mst_group_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_mst_unit_mst_group_GroupId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropForeignKey(
                name: "FK_mst_unit_mst_group_complex_MstGroupComplexId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropIndex(
                name: "IX_mst_unit_MstGroupComplexId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropIndex(
                name: "IX_mst_group_complex_MstGroupId",
                schema: "master",
                table: "mst_group_complex");

            migrationBuilder.DropIndex(
                name: "IX_mst_designation_GradeId",
                schema: "master",
                table: "mst_designation");

            migrationBuilder.DropColumn(
                name: "MstGroupComplexId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropColumn(
                name: "MstGroupId",
                schema: "master",
                table: "mst_group_complex");

            migrationBuilder.DropColumn(
                name: "GradeId",
                schema: "master",
                table: "mst_designation");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                schema: "master",
                table: "mst_unit",
                newName: "ComplexId");

            migrationBuilder.RenameIndex(
                name: "IX_mst_unit_GroupId",
                schema: "master",
                table: "mst_unit",
                newName: "IX_mst_unit_ComplexId");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                schema: "master",
                table: "mst_group_complex",
                newName: "ComplexName");

            migrationBuilder.RenameColumn(
                name: "GroupCode",
                schema: "master",
                table: "mst_group_complex",
                newName: "ComplexCode");

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                schema: "master",
                table: "mst_group_complex",
                type: "CHAR(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DesignationNameBangla",
                schema: "master",
                table: "mst_designation",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mst_group_complex_GroupId",
                schema: "master",
                table: "mst_group_complex",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_group_complex_mst_group_GroupId",
                schema: "master",
                table: "mst_group_complex",
                column: "GroupId",
                principalSchema: "master",
                principalTable: "mst_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mst_unit_mst_group_complex_ComplexId",
                schema: "master",
                table: "mst_unit",
                column: "ComplexId",
                principalSchema: "master",
                principalTable: "mst_group_complex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mst_group_complex_mst_group_GroupId",
                schema: "master",
                table: "mst_group_complex");

            migrationBuilder.DropForeignKey(
                name: "FK_mst_unit_mst_group_complex_ComplexId",
                schema: "master",
                table: "mst_unit");

            migrationBuilder.DropIndex(
                name: "IX_mst_group_complex_GroupId",
                schema: "master",
                table: "mst_group_complex");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "master",
                table: "mst_group_complex");

            migrationBuilder.DropColumn(
                name: "DesignationNameBangla",
                schema: "master",
                table: "mst_designation");

            migrationBuilder.RenameColumn(
                name: "ComplexId",
                schema: "master",
                table: "mst_unit",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_mst_unit_ComplexId",
                schema: "master",
                table: "mst_unit",
                newName: "IX_mst_unit_GroupId");

            migrationBuilder.RenameColumn(
                name: "ComplexName",
                schema: "master",
                table: "mst_group_complex",
                newName: "GroupName");

            migrationBuilder.RenameColumn(
                name: "ComplexCode",
                schema: "master",
                table: "mst_group_complex",
                newName: "GroupCode");

            migrationBuilder.AddColumn<string>(
                name: "MstGroupComplexId",
                schema: "master",
                table: "mst_unit",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MstGroupId",
                schema: "master",
                table: "mst_group_complex",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeId",
                schema: "master",
                table: "mst_designation",
                type: "CHAR(26)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_mst_unit_MstGroupComplexId",
                schema: "master",
                table: "mst_unit",
                column: "MstGroupComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_group_complex_MstGroupId",
                schema: "master",
                table: "mst_group_complex",
                column: "MstGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_designation_GradeId",
                schema: "master",
                table: "mst_designation",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_designation_mst_grade_GradeId",
                schema: "master",
                table: "mst_designation",
                column: "GradeId",
                principalSchema: "master",
                principalTable: "mst_grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mst_group_complex_mst_group_MstGroupId",
                schema: "master",
                table: "mst_group_complex",
                column: "MstGroupId",
                principalSchema: "master",
                principalTable: "mst_group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_mst_unit_mst_group_GroupId",
                schema: "master",
                table: "mst_unit",
                column: "GroupId",
                principalSchema: "master",
                principalTable: "mst_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mst_unit_mst_group_complex_MstGroupComplexId",
                schema: "master",
                table: "mst_unit",
                column: "MstGroupComplexId",
                principalSchema: "master",
                principalTable: "mst_group_complex",
                principalColumn: "Id");
        }
    }
}
