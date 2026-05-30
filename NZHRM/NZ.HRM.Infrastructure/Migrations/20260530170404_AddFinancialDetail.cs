using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFinancialDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    HouseRentAllowance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    MedicalAllowance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ConveyanceAllowance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OtherAllowance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BankAccountNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AccountType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Branch = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TinNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    IsTaxable = table.Column<bool>(type: "boolean", nullable: false),
                    TaxExempted = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NidNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    IsProvidentFundApplicable = table.Column<bool>(type: "boolean", nullable: false),
                    PfAccountNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsGratuityApplicable = table.Column<bool>(type: "boolean", nullable: false),
                    IsEsiApplicable = table.Column<bool>(type: "boolean", nullable: false),
                    SalaryEffectiveFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialDetails_EmployeeMasters_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDetails_EmployeeId",
                table: "FinancialDetails",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialDetails");
        }
    }
}
