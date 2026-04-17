using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOfferLetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationTrackings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RequisitionId = table.Column<string>(type: "text", nullable: false),
                    ApplicationTrackingRequisitionId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    ApplicantName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FatherName = table.Column<string>(type: "text", nullable: false),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    Qualification = table.Column<string>(type: "text", nullable: true),
                    CvPath = table.Column<string>(type: "text", nullable: true),
                    CirculationMedia = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTrackings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationTrackings_Requisitions_ApplicationTrackingRequis~",
                        column: x => x.ApplicationTrackingRequisitionId,
                        principalTable: "Requisitions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfferLetters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ApplicationTrackingId = table.Column<string>(type: "text", nullable: false),
                    OfferLetterApplicationTrackingId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReferenceNo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CandidateName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FatherName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Mobile = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Designation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Post = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProbationPeriod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Salary = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    JobStation = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NotificationPeriod = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SignatoryName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CompanyGroup = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferLetters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferLetters_ApplicationTrackings_OfferLetterApplicationTra~",
                        column: x => x.OfferLetterApplicationTrackingId,
                        principalTable: "ApplicationTrackings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTrackings_ApplicationTrackingRequisitionId",
                table: "ApplicationTrackings",
                column: "ApplicationTrackingRequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferLetters_OfferLetterApplicationTrackingId",
                table: "OfferLetters",
                column: "OfferLetterApplicationTrackingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferLetters");

            migrationBuilder.DropTable(
                name: "ApplicationTrackings");
        }
    }
}
