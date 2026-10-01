using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSectionToPromotionIncrementRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SectionId",
                schema: "payroll",
                table: "promotion_increment_request",
                type: "CHAR(26)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_promotion_increment_request_SectionId",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_promotion_increment_request_mst_section_SectionId",
                schema: "payroll",
                table: "promotion_increment_request",
                column: "SectionId",
                principalSchema: "master",
                principalTable: "mst_section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promotion_increment_request_mst_section_SectionId",
                schema: "payroll",
                table: "promotion_increment_request");

            migrationBuilder.DropIndex(
                name: "IX_promotion_increment_request_SectionId",
                schema: "payroll",
                table: "promotion_increment_request");

            migrationBuilder.DropColumn(
                name: "SectionId",
                schema: "payroll",
                table: "promotion_increment_request");
        }
    }
}
