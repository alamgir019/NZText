using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZ.HRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "recruitment");

            migrationBuilder.EnsureSchema(
                name: "workflow");

            migrationBuilder.EnsureSchema(
                name: "audit");

            migrationBuilder.EnsureSchema(
                name: "payroll");

            migrationBuilder.EnsureSchema(
                name: "attendance");

            migrationBuilder.EnsureSchema(
                name: "lookup");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.EnsureSchema(
                name: "hrm");

            migrationBuilder.EnsureSchema(
                name: "leave_mgmt");

            migrationBuilder.EnsureSchema(
                name: "master");

            migrationBuilder.CreateTable(
                name: "blood_group",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    BloodGroupCode = table.Column<string>(type: "text", nullable: false),
                    BloodGroupName = table.Column<string>(type: "text", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blood_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "data_change",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    RecordId = table.Column<string>(type: "text", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: false),
                    OldValue = table.Column<string>(type: "text", nullable: true),
                    NewValue = table.Column<string>(type: "text", nullable: true),
                    ChangedBy = table.Column<string>(type: "text", nullable: true),
                    ChangeDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_change", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "delegation",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FromUserId = table.Column<string>(type: "text", nullable: false),
                    ToUserId = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    WorkflowMasterId = table.Column<string>(type: "text", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delegation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "division",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DivisionName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_division", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employee_nature",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    NatureName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_nature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "holiday_calendar",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    HolidayDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HolidayName = table.Column<string>(type: "text", nullable: false),
                    HolidayType = table.Column<string>(type: "text", nullable: true),
                    UnitId = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_holiday_calendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "holiday_calendar",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    HolidayDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HolidayName = table.Column<string>(type: "text", nullable: false),
                    HolidayType = table.Column<string>(type: "text", nullable: true),
                    UnitId = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_holiday_calendar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "interview_panel",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PanelName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_panel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "job_position",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PositionCode = table.Column<string>(type: "text", nullable: false),
                    PositionName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leave_type",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LeaveName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LeaveCategory = table.Column<string>(type: "text", nullable: true),
                    AnnualEntitlement = table.Column<decimal>(type: "numeric", nullable: false),
                    Encashable = table.Column<bool>(type: "boolean", nullable: false),
                    CarryForwardAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    MaxCarryForwardDays = table.Column<decimal>(type: "numeric", nullable: false),
                    ApprovalRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leave_year",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveYearValue = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsCurrentYear = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_year", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_department",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SubunitId = table.Column<string>(type: "text", nullable: false),
                    DepartmentCode = table.Column<string>(type: "text", nullable: false),
                    DepartmentName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_employee_category",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CategoryCode = table.Column<string>(type: "text", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: false),
                    OtEligible = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_employee_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_grade",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    GradeCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    GradeName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    MinimumSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    MaximumSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    EmployeeType = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_grade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_group",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    GroupCode = table.Column<string>(type: "text", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_section",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SectionCode = table.Column<string>(type: "text", nullable: false),
                    SectionName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_section", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mst_shift",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ShiftCode = table.Column<string>(type: "text", nullable: false),
                    ShiftName = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    GraceMinutes = table.Column<int>(type: "integer", nullable: false),
                    FullDayHours = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_shift", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payroll_header",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    GroupId = table.Column<string>(type: "text", nullable: true),
                    UnitId = table.Column<string>(type: "text", nullable: true),
                    PayrollStatus = table.Column<string>(type: "text", nullable: true),
                    TotalEmployees = table.Column<int>(type: "integer", nullable: false),
                    TotalGross = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDeduction = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNetSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_header", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payroll_lock",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<string>(type: "text", nullable: true),
                    LockedBy = table.Column<string>(type: "text", nullable: true),
                    LockDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UnlockBy = table.Column<string>(type: "text", nullable: true),
                    UnlockDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_lock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payroll_process_log",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmployeeCount = table.Column<int>(type: "integer", nullable: false),
                    ProcessedCount = table.Column<int>(type: "integer", nullable: false),
                    ExceptionCount = table.Column<int>(type: "integer", nullable: false),
                    ProcessedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_process_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payroll_processing_group",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ProcessingGroupCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProcessingGroupName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_processing_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PermissionCode = table.Column<string>(type: "text", nullable: false),
                    PermissionName = table.Column<string>(type: "text", nullable: false),
                    ModuleName = table.Column<string>(type: "text", nullable: true),
                    PermissionType = table.Column<string>(type: "text", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "physical_examination_setting",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FieldName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    FieldType = table.Column<int>(type: "integer", nullable: false),
                    OptionValuesJson = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_physical_examination_setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "processing_log",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ProcessDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalEmployees = table.Column<int>(type: "integer", nullable: false),
                    TotalProcessed = table.Column<int>(type: "integer", nullable: false),
                    TotalExceptions = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProcessedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processing_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RoleCode = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "special_payroll_policy",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PolicyCode = table.Column<string>(type: "text", nullable: false),
                    PolicyName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PercentageOfSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_special_payroll_policy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_account",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    LoginId = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "workflow_master",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowCode = table.Column<string>(type: "text", nullable: false),
                    WorkflowName = table.Column<string>(type: "text", nullable: false),
                    ModuleName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_master", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "district",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DistrictName = table.Column<string>(type: "text", nullable: false),
                    DivisionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_district", x => x.Id);
                    table.ForeignKey(
                        name: "FK_district_division_DivisionId",
                        column: x => x.DivisionId,
                        principalSchema: "lookup",
                        principalTable: "division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidate",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CandidateCode = table.Column<string>(type: "text", nullable: false),
                    CandidateName = table.Column<string>(type: "text", nullable: false),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    GenderId = table.Column<string>(type: "text", nullable: true),
                    ExpectedSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    CurrentSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    PositionId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_candidate_job_position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "recruitment",
                        principalTable: "job_position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "manpower_requisition",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RequisitionCode = table.Column<string>(type: "text", nullable: false),
                    PositionId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    VacancyCount = table.Column<int>(type: "integer", nullable: false),
                    RequisitionReason = table.Column<string>(type: "text", nullable: true),
                    RequestedBy = table.Column<string>(type: "text", nullable: true),
                    RequestedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manpower_requisition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_manpower_requisition_job_position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "recruitment",
                        principalTable: "job_position",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "leave_policy",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveTypeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeCategoryId = table.Column<string>(type: "text", nullable: true),
                    AnnualEntitlement = table.Column<decimal>(type: "numeric", nullable: false),
                    CarryForwardAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    MaxCarryForwardDays = table.Column<decimal>(type: "numeric", nullable: false),
                    EncashAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovalLevels = table.Column<int>(type: "integer", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_policy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_policy_leave_type_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mst_designation",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DesignationCode = table.Column<string>(type: "text", nullable: false),
                    DesignationName = table.Column<string>(type: "text", nullable: false),
                    GradeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    OtEligible = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_designation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_designation_mst_grade_GradeId",
                        column: x => x.GradeId,
                        principalSchema: "master",
                        principalTable: "mst_grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mst_unit",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    GroupId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UnitCode = table.Column<string>(type: "text", nullable: false),
                    UnitName = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_unit_mst_group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "master",
                        principalTable: "mst_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mst_cell",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    NameEnglish = table.Column<string>(type: "text", nullable: false),
                    SectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CellCode = table.Column<string>(type: "text", nullable: false),
                    CellName = table.Column<string>(type: "text", nullable: false),
                    NameBangla = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_cell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_cell_mst_section_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "master",
                        principalTable: "mst_section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mst_department_section",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SectionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_department_section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_department_section_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "master",
                        principalTable: "mst_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mst_department_section_mst_section_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "master",
                        principalTable: "mst_section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_master",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeCode = table.Column<string>(type: "text", nullable: false),
                    EnrollmentId = table.Column<string>(type: "text", nullable: false),
                    CardNo = table.Column<string>(type: "text", nullable: false),
                    OldCardNo = table.Column<string>(type: "text", nullable: true),
                    EmployeeName = table.Column<string>(type: "text", nullable: false),
                    EmployeeNameBangla = table.Column<string>(type: "text", nullable: false),
                    EmployeeNameEnglish = table.Column<string>(type: "text", nullable: false),
                    MstPayrollProcessingGroupId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_master", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_master_payroll_processing_group_MstPayrollProcessi~",
                        column: x => x.MstPayrollProcessingGroupId,
                        principalSchema: "master",
                        principalTable: "payroll_processing_group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "field_security",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RoleId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ScreenCode = table.Column<string>(type: "text", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: false),
                    CanView = table.Column<bool>(type: "boolean", nullable: false),
                    CanEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_security", x => x.Id);
                    table.ForeignKey(
                        name: "FK_field_security_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "module_access",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RoleId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ModuleCode = table.Column<string>(type: "text", nullable: false),
                    CanView = table.Column<bool>(type: "boolean", nullable: false),
                    CanAdd = table.Column<bool>(type: "boolean", nullable: false),
                    CanEdit = table.Column<bool>(type: "boolean", nullable: false),
                    CanDelete = table.Column<bool>(type: "boolean", nullable: false),
                    CanApprove = table.Column<bool>(type: "boolean", nullable: false),
                    CanExport = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module_access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_module_access_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RoleId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PermissionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_permission_permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "security",
                        principalTable: "permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permission_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "special_payroll_band",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PolicyId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    BandName = table.Column<string>(type: "text", nullable: false),
                    FromPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    ToPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_special_payroll_band", x => x.Id);
                    table.ForeignKey(
                        name: "FK_special_payroll_band_special_payroll_policy_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "payroll",
                        principalTable: "special_payroll_policy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emergency_access",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    GrantedBy = table.Column<string>(type: "text", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emergency_access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emergency_access_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "export_history",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ModuleName = table.Column<string>(type: "text", nullable: true),
                    ExportType = table.Column<string>(type: "text", nullable: true),
                    ExportDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RecordCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_export_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_export_history_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "login_history",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LoginDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LogoutDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    LoginStatus = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_login_history_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "password_history",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_password_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_password_history_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_access",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ReportName = table.Column<string>(type: "text", nullable: false),
                    AccessDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExportFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_report_access_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "system_event",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: true),
                    EventDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    EventDescription = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_system_event_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RoleId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_role_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "security",
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_session",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UserId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LoginDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LogoutDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    DeviceInfo = table.Column<string>(type: "text", nullable: true),
                    SessionStatus = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_session_user_account_UserId",
                        column: x => x.UserId,
                        principalSchema: "security",
                        principalTable: "user_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "escalation_rule",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowMasterId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    StepNo = table.Column<int>(type: "integer", nullable: false),
                    EscalateAfterHours = table.Column<int>(type: "integer", nullable: false),
                    EscalateToRoleId = table.Column<string>(type: "text", nullable: true),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_escalation_rule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_escalation_rule_workflow_master_WorkflowMasterId",
                        column: x => x.WorkflowMasterId,
                        principalSchema: "workflow",
                        principalTable: "workflow_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_step",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowMasterId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    StepNo = table.Column<int>(type: "integer", nullable: false),
                    StepName = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    MandatoryFlag = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workflow_step_workflow_master_WorkflowMasterId",
                        column: x => x.WorkflowMasterId,
                        principalSchema: "workflow",
                        principalTable: "workflow_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_transaction",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowMasterId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ReferenceTable = table.Column<string>(type: "text", nullable: true),
                    ReferenceId = table.Column<string>(type: "text", nullable: true),
                    RequestorId = table.Column<string>(type: "text", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CurrentStepNo = table.Column<int>(type: "integer", nullable: false),
                    CurrentApproverId = table.Column<string>(type: "text", nullable: true),
                    WorkflowStatus = table.Column<string>(type: "text", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workflow_transaction_workflow_master_WorkflowMasterId",
                        column: x => x.WorkflowMasterId,
                        principalSchema: "workflow",
                        principalTable: "workflow_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thana",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ThanaName = table.Column<string>(type: "text", nullable: false),
                    DistrictId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thana", x => x.Id);
                    table.ForeignKey(
                        name: "FK_thana_district_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "lookup",
                        principalTable: "district",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CandidateId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AppointmentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EmployeeId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appointment_candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "recruitment",
                        principalTable: "candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidate_document",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CandidateId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DocumentTypeId = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate_document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_candidate_document_candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "recruitment",
                        principalTable: "candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interview_schedule",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CandidateId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    InterviewDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InterviewPanelId = table.Column<string>(type: "text", nullable: true),
                    InterviewerId = table.Column<string>(type: "text", nullable: true),
                    InterviewType = table.Column<string>(type: "text", nullable: true),
                    RecInterviewPanelId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interview_schedule_candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "recruitment",
                        principalTable: "candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_interview_schedule_interview_panel_RecInterviewPanelId",
                        column: x => x.RecInterviewPanelId,
                        principalSchema: "recruitment",
                        principalTable: "interview_panel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "offer_letter",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CandidateId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    OfferDetails = table.Column<string>(type: "text", nullable: true),
                    OfferDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offer_letter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_offer_letter_candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "recruitment",
                        principalTable: "candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recruitment_workflow",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CandidateId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recruitment_workflow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recruitment_workflow_candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "recruitment",
                        principalTable: "candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendance_lock",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AttendanceMonth = table.Column<string>(type: "text", nullable: false),
                    UnitId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    LockDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LockedBy = table.Column<string>(type: "text", nullable: true),
                    UnlockDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UnlockedBy = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance_lock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendance_lock_mst_unit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "master",
                        principalTable: "mst_unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "device_master",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DeviceCode = table.Column<string>(type: "text", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    UnitId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    LastSyncTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_master", x => x.Id);
                    table.ForeignKey(
                        name: "FK_device_master_mst_unit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "master",
                        principalTable: "mst_unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_subunit",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    UnitId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SubunitCode = table.Column<string>(type: "text", nullable: false),
                    SubunitName = table.Column<string>(type: "text", nullable: false),
                    DistrictId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_subunit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_subunit_district_DistrictId",
                        column: x => x.DistrictId,
                        principalSchema: "lookup",
                        principalTable: "district",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_mst_subunit_mst_unit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "master",
                        principalTable: "mst_unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "arrear",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    ArrearType = table.Column<string>(type: "text", nullable: true),
                    ArrearAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arrear", x => x.Id);
                    table.ForeignKey(
                        name: "FK_arrear_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendance_adjustment",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AttendanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AdjustmentType = table.Column<string>(type: "text", nullable: true),
                    OldValue = table.Column<string>(type: "text", nullable: true),
                    NewValue = table.Column<string>(type: "text", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    RequestedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance_adjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendance_adjustment_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendance_exception",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AttendanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExceptionType = table.Column<string>(type: "text", nullable: true),
                    Severity = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    ResolvedFlag = table.Column<bool>(type: "boolean", nullable: false),
                    ResolvedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendance_exception", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attendance_exception_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bank_transfer",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    BankName = table.Column<string>(type: "text", nullable: true),
                    AccountNo = table.Column<string>(type: "text", nullable: true),
                    TransferAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransferStatus = table.Column<string>(type: "text", nullable: true),
                    TransferDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_transfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bank_transfer_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bank_transfer_payroll_header_PayrollId",
                        column: x => x.PayrollId,
                        principalSchema: "payroll",
                        principalTable: "payroll_header",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bonus",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    BonusType = table.Column<string>(type: "text", nullable: true),
                    BonusAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    BonusDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PayrollMonth = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bonus_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_bank_account",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    BankId = table.Column<string>(type: "text", nullable: true),
                    AccountName = table.Column<string>(type: "text", nullable: true),
                    AccountNo = table.Column<string>(type: "text", nullable: true),
                    RoutingNo = table.Column<string>(type: "text", nullable: true),
                    BranchName = table.Column<string>(type: "text", nullable: true),
                    MobileBankingFlag = table.Column<bool>(type: "boolean", nullable: false),
                    SalaryAccountFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_bank_account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_bank_account_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_contact",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    EmergencyContactNo = table.Column<string>(type: "text", nullable: true),
                    PersonalEmail = table.Column<string>(type: "text", nullable: true),
                    PresentAddress = table.Column<string>(type: "text", nullable: true),
                    PermanentAddress = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "employee_document",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DocumentTypeId = table.Column<string>(type: "text", nullable: true),
                    DocumentNo = table.Column<string>(type: "text", nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_document_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_education",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EducationLevelId = table.Column<string>(type: "text", nullable: true),
                    InstituteName = table.Column<string>(type: "text", nullable: true),
                    BoardUniversity = table.Column<string>(type: "text", nullable: true),
                    PassingYear = table.Column<int>(type: "integer", nullable: true),
                    ResultGpa = table.Column<string>(type: "text", nullable: true),
                    MajorSubject = table.Column<string>(type: "text", nullable: true),
                    CertificateNo = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_education_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_experience",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    Designation = table.Column<string>(type: "text", nullable: true),
                    JoiningDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LeavingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LastSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    Responsibilities = table.Column<string>(type: "text", nullable: true),
                    ReasonForLeaving = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_experience_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_family",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FamilyMemberName = table.Column<string>(type: "text", nullable: false),
                    Relationship = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Occupation = table.Column<string>(type: "text", nullable: true),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    DependentFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_family", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_family_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_nominee",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    NomineeName = table.Column<string>(type: "text", nullable: false),
                    Relationship = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    NidNo = table.Column<string>(type: "text", nullable: true),
                    MobileNo = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    NominationPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_nominee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_nominee_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_payroll",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    BankId = table.Column<string>(type: "text", nullable: true),
                    BankAccountNo = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_payroll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_payroll_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_personal",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FatherName = table.Column<string>(type: "text", nullable: true),
                    MotherName = table.Column<string>(type: "text", nullable: true),
                    SpouseName = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Religion = table.Column<string>(type: "text", nullable: true),
                    MaritalStatus = table.Column<string>(type: "text", nullable: true),
                    BloodGroup = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    NidNo = table.Column<string>(type: "text", nullable: true),
                    BirthCertificateNo = table.Column<string>(type: "text", nullable: true),
                    PassportNo = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_personal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_personal_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_reporting",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ReportingEmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ReportingType = table.Column<string>(type: "text", nullable: true),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_reporting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_reporting_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_reporting_employee_master_ReportingEmployeeId",
                        column: x => x.ReportingEmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employee_training",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    TrainingName = table.Column<string>(type: "text", nullable: true),
                    TrainingProvider = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TrainingHours = table.Column<decimal>(type: "numeric", nullable: true),
                    CertificateReceived = table.Column<bool>(type: "boolean", nullable: false),
                    CertificateNo = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_training_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_verification",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SecurityClearanceBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SecurityClearanceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EnrolledBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EnrolledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BiometricEnrolledBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BiometricEnrolledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_verification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_verification_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "increment_history",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OldGrossSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    NewGrossSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    IncrementAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    IncrementPercent = table.Column<decimal>(type: "numeric", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_increment_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_increment_history_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inside_factory_status",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LastPunchTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CurrentStatus = table.Column<string>(type: "text", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inside_factory_status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inside_factory_status_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_accrual",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveTypeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AccrualMonth = table.Column<string>(type: "text", nullable: false),
                    AccruedDays = table.Column<decimal>(type: "numeric", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_accrual", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_accrual_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_leave_accrual_leave_type_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_adjustment",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveTypeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AdjustmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AdjustmentDays = table.Column<decimal>(type: "numeric", nullable: false),
                    AdjustmentReason = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_adjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_adjustment_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_leave_adjustment_leave_type_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_application",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveTypeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalDays = table.Column<decimal>(type: "numeric", nullable: false),
                    LeaveReason = table.Column<string>(type: "text", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WorkflowId = table.Column<string>(type: "text", nullable: true),
                    LeaveStatus = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_application_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_leave_application_leave_type_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_balance",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveTypeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    YearId = table.Column<int>(type: "integer", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    EarnedLeave = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailedLeave = table.Column<decimal>(type: "numeric", nullable: false),
                    AdjustedLeave = table.Column<decimal>(type: "numeric", nullable: false),
                    EncashedLeave = table.Column<decimal>(type: "numeric", nullable: false),
                    ClosingBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_balance_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_leave_balance_leave_type_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_encashment",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveTypeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EncashDays = table.Column<decimal>(type: "numeric", nullable: false),
                    EncashAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    EncashDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_encashment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_encashment_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_leave_encashment_leave_type_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_opening_balance",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveTypeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveYear = table.Column<int>(type: "integer", nullable: false),
                    OpeningDays = table.Column<decimal>(type: "numeric", nullable: false),
                    AllocationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_opening_balance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_opening_balance_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_leave_opening_balance_leave_type_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_fitness_check",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EnrollmentId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BloodGroupId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    HeightCm = table.Column<decimal>(type: "numeric", nullable: true),
                    WeightKg = table.Column<decimal>(type: "numeric", nullable: true),
                    PhysicalExaminationDataJson = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsFit = table.Column<bool>(type: "boolean", nullable: false),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ExaminedByDoctor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExaminationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_fitness_check", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_fitness_check_blood_group_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalSchema: "lookup",
                        principalTable: "blood_group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_medical_fitness_check_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ot_authorization",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    OtDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ApprovedStartTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    ApprovedEndTime = table.Column<TimeSpan>(type: "interval", nullable: true),
                    ApprovedHours = table.Column<decimal>(type: "numeric", nullable: true),
                    RequestedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ot_authorization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ot_authorization_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "partial_salary_payment",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollPeriod = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partial_salary_payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_partial_salary_payment_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payroll_adjustment",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    AdjustmentType = table.Column<string>(type: "text", nullable: true),
                    OldAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    NewAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    AdjustmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_adjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payroll_adjustment_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payroll_details",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    PayableDays = table.Column<decimal>(type: "numeric", nullable: false),
                    WorkedDays = table.Column<decimal>(type: "numeric", nullable: false),
                    OTAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    BonusAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    ArrearAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    DeductionAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    LoanRecovery = table.Column<decimal>(type: "numeric", nullable: false),
                    NetSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payroll_details_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payroll_details_payroll_header_PayrollId",
                        column: x => x.PayrollId,
                        principalSchema: "payroll",
                        principalTable: "payroll_header",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payroll_exception",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollPeriod = table.Column<string>(type: "text", nullable: false),
                    ExceptionType = table.Column<string>(type: "text", nullable: true),
                    ExceptionDescription = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    ResolvedBy = table.Column<string>(type: "text", nullable: true),
                    ResolvedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payroll_exception", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payroll_exception_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "processed_attendance",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    AttendanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ShiftId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    ActualInTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ActualOutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PayableInTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PayableOutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WorkedHours = table.Column<decimal>(type: "numeric", nullable: false),
                    OtWorkedHours = table.Column<decimal>(type: "numeric", nullable: false),
                    OtPayableHours = table.Column<decimal>(type: "numeric", nullable: false),
                    AttendanceStatus = table.Column<string>(type: "text", nullable: true),
                    ProcessingStatus = table.Column<string>(type: "text", nullable: true),
                    ProcessedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processed_attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_processed_attendance_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_processed_attendance_mst_shift_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "master",
                        principalTable: "mst_shift",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "salary_structure",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BasicSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    HouseRent = table.Column<decimal>(type: "numeric", nullable: false),
                    MedicalAllowance = table.Column<decimal>(type: "numeric", nullable: false),
                    ConveyanceAllowance = table.Column<decimal>(type: "numeric", nullable: false),
                    FoodAllowance = table.Column<decimal>(type: "numeric", nullable: false),
                    OtherAllowance = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary_structure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_salary_structure_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shift_roster",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ShiftId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RosterDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AssignedBy = table.Column<string>(type: "text", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shift_roster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shift_roster_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shift_roster_mst_shift_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "master",
                        principalTable: "mst_shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tax",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    TaxableIncome = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxRuleId = table.Column<string>(type: "text", nullable: true),
                    CalculationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tax_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "weekly_off_pattern",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DayOfWeek = table.Column<string>(type: "text", nullable: false),
                    EffectiveDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weekly_off_pattern", x => x.Id);
                    table.ForeignKey(
                        name: "FK_weekly_off_pattern_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approval_history",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowTransactionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    StepNo = table.Column<int>(type: "integer", nullable: false),
                    ApproverId = table.Column<string>(type: "text", nullable: true),
                    ActionTaken = table.Column<string>(type: "text", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_approval_history_workflow_transaction_WorkflowTransactionId",
                        column: x => x.WorkflowTransactionId,
                        principalSchema: "workflow",
                        principalTable: "workflow_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approval_trail",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowTransactionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ApproverId = table.Column<string>(type: "text", nullable: true),
                    ActionTaken = table.Column<string>(type: "text", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_trail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_approval_trail_workflow_transaction_WorkflowTransactionId",
                        column: x => x.WorkflowTransactionId,
                        principalSchema: "workflow",
                        principalTable: "workflow_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification_queue",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowTransactionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    NotificationType = table.Column<string>(type: "text", nullable: true),
                    NotificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryStatus = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_queue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notification_queue_workflow_transaction_WorkflowTransaction~",
                        column: x => x.WorkflowTransactionId,
                        principalSchema: "workflow",
                        principalTable: "workflow_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pending_approval",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowTransactionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    ApproverId = table.Column<string>(type: "text", nullable: false),
                    PendingSince = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PriorityLevel = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pending_approval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pending_approval_workflow_transaction_WorkflowTransactionId",
                        column: x => x.WorkflowTransactionId,
                        principalSchema: "workflow",
                        principalTable: "workflow_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_attachment",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowTransactionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UploadedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workflow_attachment_workflow_transaction_WorkflowTransactio~",
                        column: x => x.WorkflowTransactionId,
                        principalSchema: "workflow",
                        principalTable: "workflow_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workflow_audit",
                schema: "workflow",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowTransactionId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EventType = table.Column<string>(type: "text", nullable: true),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    EventDetails = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workflow_audit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_workflow_audit_workflow_transaction_WorkflowTransactionId",
                        column: x => x.WorkflowTransactionId,
                        principalSchema: "workflow",
                        principalTable: "workflow_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interview_evaluation",
                schema: "recruitment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    InterviewScheduleId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EvaluatorId = table.Column<string>(type: "text", nullable: true),
                    Evaluation = table.Column<string>(type: "text", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_evaluation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_interview_evaluation_interview_schedule_InterviewScheduleId",
                        column: x => x.InterviewScheduleId,
                        principalSchema: "recruitment",
                        principalTable: "interview_schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "device_sync_log",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DeviceId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SyncStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SyncEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PunchCount = table.Column<int>(type: "integer", nullable: false),
                    SyncStatus = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_device_sync_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_device_sync_log_device_master_DeviceId",
                        column: x => x.DeviceId,
                        principalSchema: "attendance",
                        principalTable: "device_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "raw_punch",
                schema: "attendance",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "text", nullable: true),
                    CardNo = table.Column<string>(type: "text", nullable: true),
                    PunchDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PunchDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PunchTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DeviceId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    DeviceLocation = table.Column<string>(type: "text", nullable: true),
                    VerificationMode = table.Column<string>(type: "text", nullable: true),
                    PunchSource = table.Column<string>(type: "text", nullable: true),
                    ImportBatchId = table.Column<string>(type: "text", nullable: true),
                    PunchStatus = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_raw_punch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_raw_punch_device_master_DeviceId",
                        column: x => x.DeviceId,
                        principalSchema: "attendance",
                        principalTable: "device_master",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "employee_employment",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    JoiningDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ConfirmationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ResignationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    SeparationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GroupId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    UnitId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    SubunitId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    SectionId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CellId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    DesignationId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    GradeId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    ShiftId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    EmployeeCategoryId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    ReportingEmployeeId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    ProcessingGroupId = table.Column<string>(type: "CHAR(26)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_employment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_employment_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_employment_employee_master_ReportingEmployeeId",
                        column: x => x.ReportingEmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_cell_CellId",
                        column: x => x.CellId,
                        principalSchema: "master",
                        principalTable: "mst_cell",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "master",
                        principalTable: "mst_department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_designation_DesignationId",
                        column: x => x.DesignationId,
                        principalSchema: "master",
                        principalTable: "mst_designation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_employee_category_EmployeeCategoryId",
                        column: x => x.EmployeeCategoryId,
                        principalSchema: "master",
                        principalTable: "mst_employee_category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_grade_GradeId",
                        column: x => x.GradeId,
                        principalSchema: "master",
                        principalTable: "mst_grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "master",
                        principalTable: "mst_group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_section_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "master",
                        principalTable: "mst_section",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_shift_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "master",
                        principalTable: "mst_shift",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_subunit_SubunitId",
                        column: x => x.SubunitId,
                        principalSchema: "master",
                        principalTable: "mst_subunit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_mst_unit_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "master",
                        principalTable: "mst_unit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_employment_payroll_processing_group_ProcessingGrou~",
                        column: x => x.ProcessingGroupId,
                        principalSchema: "master",
                        principalTable: "payroll_processing_group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "mst_subunit_department",
                schema: "master",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    SubunitId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DepartmentId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mst_subunit_department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mst_subunit_department_mst_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "master",
                        principalTable: "mst_department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mst_subunit_department_mst_subunit_SubunitId",
                        column: x => x.SubunitId,
                        principalSchema: "master",
                        principalTable: "mst_subunit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_application_details",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveApplicationId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LeaveFraction = table.Column<decimal>(type: "numeric", nullable: false),
                    LeaveDayType = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_application_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_application_details_leave_application_LeaveApplicatio~",
                        column: x => x.LeaveApplicationId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_approval_history",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveApplicationId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    WorkflowStepNo = table.Column<int>(type: "integer", nullable: false),
                    ApproverId = table.Column<string>(type: "text", nullable: true),
                    ActionTaken = table.Column<string>(type: "text", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_approval_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_approval_history_leave_application_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leave_cancellation",
                schema: "leave_mgmt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LeaveApplicationId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    CancellationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancelledBy = table.Column<string>(type: "text", nullable: true),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leave_cancellation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leave_cancellation_leave_application_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalSchema: "leave_mgmt",
                        principalTable: "leave_application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deduction",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollDetailId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    DeductionType = table.Column<string>(type: "text", nullable: true),
                    DeductionAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_deduction_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_deduction_payroll_details_PayrollDetailId",
                        column: x => x.PayrollDetailId,
                        principalSchema: "payroll",
                        principalTable: "payroll_details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loan_recovery",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollDetailId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    LoanId = table.Column<string>(type: "text", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    RecoveryAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    BalanceAfterRecovery = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_recovery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_loan_recovery_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_loan_recovery_payroll_details_PayrollDetailId",
                        column: x => x.PayrollDetailId,
                        principalSchema: "payroll",
                        principalTable: "payroll_details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ot_details",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollDetailId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    TotalOTHours = table.Column<decimal>(type: "numeric", nullable: false),
                    OTRate = table.Column<decimal>(type: "numeric", nullable: false),
                    OTAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ot_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ot_details_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ot_details_payroll_details_PayrollDetailId",
                        column: x => x.PayrollDetailId,
                        principalSchema: "payroll",
                        principalTable: "payroll_details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payslip",
                schema: "payroll",
                columns: table => new
                {
                    Id = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollDetailId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    EmployeeId = table.Column<string>(type: "CHAR(26)", nullable: false),
                    PayrollMonth = table.Column<string>(type: "text", nullable: false),
                    PayslipFilePath = table.Column<string>(type: "text", nullable: true),
                    GeneratedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GeneratedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payslip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payslip_employee_master_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "employee_master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payslip_payroll_details_PayrollDetailId",
                        column: x => x.PayrollDetailId,
                        principalSchema: "payroll",
                        principalTable: "payroll_details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointment_CandidateId",
                schema: "recruitment",
                table: "appointment",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_approval_history_WorkflowTransactionId",
                schema: "workflow",
                table: "approval_history",
                column: "WorkflowTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_approval_trail_WorkflowTransactionId",
                schema: "audit",
                table: "approval_trail",
                column: "WorkflowTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_arrear_EmployeeId",
                schema: "payroll",
                table: "arrear",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_adjustment_EmployeeId",
                schema: "attendance",
                table: "attendance_adjustment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_exception_EmployeeId",
                schema: "attendance",
                table: "attendance_exception",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_attendance_lock_UnitId",
                schema: "attendance",
                table: "attendance_lock",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_bank_transfer_EmployeeId",
                schema: "payroll",
                table: "bank_transfer",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_bank_transfer_PayrollId",
                schema: "payroll",
                table: "bank_transfer",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_bonus_EmployeeId",
                schema: "payroll",
                table: "bonus",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_candidate_PositionId",
                schema: "recruitment",
                table: "candidate",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_candidate_document_CandidateId",
                schema: "recruitment",
                table: "candidate_document",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_deduction_EmployeeId",
                schema: "payroll",
                table: "deduction",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_deduction_PayrollDetailId",
                schema: "payroll",
                table: "deduction",
                column: "PayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_device_master_UnitId",
                schema: "attendance",
                table: "device_master",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_device_sync_log_DeviceId",
                schema: "attendance",
                table: "device_sync_log",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_district_DivisionId",
                schema: "lookup",
                table: "district",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_emergency_access_UserId",
                schema: "security",
                table: "emergency_access",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_bank_account_EmployeeId",
                schema: "hrm",
                table: "employee_bank_account",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_contact_EmployeeId",
                schema: "hrm",
                table: "employee_contact",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_document_EmployeeId",
                schema: "hrm",
                table: "employee_document",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_education_EmployeeId",
                schema: "hrm",
                table: "employee_education",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_CellId",
                schema: "hrm",
                table: "employee_employment",
                column: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_DepartmentId",
                schema: "hrm",
                table: "employee_employment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_DesignationId",
                schema: "hrm",
                table: "employee_employment",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_EmployeeCategoryId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_EmployeeId",
                schema: "hrm",
                table: "employee_employment",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_GradeId",
                schema: "hrm",
                table: "employee_employment",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_GroupId",
                schema: "hrm",
                table: "employee_employment",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_ProcessingGroupId",
                schema: "hrm",
                table: "employee_employment",
                column: "ProcessingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_ReportingEmployeeId",
                schema: "hrm",
                table: "employee_employment",
                column: "ReportingEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_SectionId",
                schema: "hrm",
                table: "employee_employment",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_ShiftId",
                schema: "hrm",
                table: "employee_employment",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_SubunitId",
                schema: "hrm",
                table: "employee_employment",
                column: "SubunitId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_employment_UnitId",
                schema: "hrm",
                table: "employee_employment",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_experience_EmployeeId",
                schema: "hrm",
                table: "employee_experience",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_family_EmployeeId",
                schema: "hrm",
                table: "employee_family",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_master_MstPayrollProcessingGroupId",
                schema: "hrm",
                table: "employee_master",
                column: "MstPayrollProcessingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_nominee_EmployeeId",
                schema: "hrm",
                table: "employee_nominee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_payroll_EmployeeId",
                schema: "hrm",
                table: "employee_payroll",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_personal_EmployeeId",
                schema: "hrm",
                table: "employee_personal",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_reporting_EmployeeId",
                schema: "hrm",
                table: "employee_reporting",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_reporting_ReportingEmployeeId",
                schema: "hrm",
                table: "employee_reporting",
                column: "ReportingEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_training_EmployeeId",
                schema: "hrm",
                table: "employee_training",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_verification_EmployeeId",
                schema: "hrm",
                table: "employee_verification",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_escalation_rule_WorkflowMasterId",
                schema: "workflow",
                table: "escalation_rule",
                column: "WorkflowMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_export_history_UserId",
                schema: "audit",
                table: "export_history",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_field_security_RoleId",
                schema: "security",
                table: "field_security",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_increment_history_EmployeeId",
                schema: "payroll",
                table: "increment_history",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_inside_factory_status_EmployeeId",
                schema: "attendance",
                table: "inside_factory_status",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_interview_evaluation_InterviewScheduleId",
                schema: "recruitment",
                table: "interview_evaluation",
                column: "InterviewScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_interview_schedule_CandidateId",
                schema: "recruitment",
                table: "interview_schedule",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_interview_schedule_RecInterviewPanelId",
                schema: "recruitment",
                table: "interview_schedule",
                column: "RecInterviewPanelId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_accrual_EmployeeId",
                schema: "leave_mgmt",
                table: "leave_accrual",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_accrual_LeaveTypeId",
                schema: "leave_mgmt",
                table: "leave_accrual",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_adjustment_EmployeeId",
                schema: "leave_mgmt",
                table: "leave_adjustment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_adjustment_LeaveTypeId",
                schema: "leave_mgmt",
                table: "leave_adjustment",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_application_EmployeeId",
                schema: "leave_mgmt",
                table: "leave_application",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_application_LeaveTypeId",
                schema: "leave_mgmt",
                table: "leave_application",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_application_details_LeaveApplicationId",
                schema: "leave_mgmt",
                table: "leave_application_details",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_approval_history_LeaveApplicationId",
                schema: "leave_mgmt",
                table: "leave_approval_history",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_balance_EmployeeId",
                schema: "leave_mgmt",
                table: "leave_balance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_balance_LeaveTypeId",
                schema: "leave_mgmt",
                table: "leave_balance",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_cancellation_LeaveApplicationId",
                schema: "leave_mgmt",
                table: "leave_cancellation",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_encashment_EmployeeId",
                schema: "leave_mgmt",
                table: "leave_encashment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_encashment_LeaveTypeId",
                schema: "leave_mgmt",
                table: "leave_encashment",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_opening_balance_EmployeeId",
                schema: "leave_mgmt",
                table: "leave_opening_balance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_opening_balance_LeaveTypeId",
                schema: "leave_mgmt",
                table: "leave_opening_balance",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_leave_policy_LeaveTypeId",
                schema: "leave_mgmt",
                table: "leave_policy",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_loan_recovery_EmployeeId",
                schema: "payroll",
                table: "loan_recovery",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_loan_recovery_PayrollDetailId",
                schema: "payroll",
                table: "loan_recovery",
                column: "PayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_login_history_UserId",
                schema: "audit",
                table: "login_history",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_manpower_requisition_PositionId",
                schema: "recruitment",
                table: "manpower_requisition",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_medical_fitness_check_BloodGroupId",
                schema: "hrm",
                table: "medical_fitness_check",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_medical_fitness_check_EmployeeId",
                schema: "hrm",
                table: "medical_fitness_check",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_module_access_RoleId",
                schema: "security",
                table: "module_access",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_cell_SectionId",
                schema: "master",
                table: "mst_cell",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_section_DepartmentId",
                schema: "master",
                table: "mst_department_section",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_department_section_SectionId",
                schema: "master",
                table: "mst_department_section",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_designation_GradeId",
                schema: "master",
                table: "mst_designation",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_subunit_DistrictId",
                schema: "master",
                table: "mst_subunit",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_subunit_UnitId",
                schema: "master",
                table: "mst_subunit",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_subunit_department_DepartmentId",
                schema: "master",
                table: "mst_subunit_department",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_subunit_department_SubunitId",
                schema: "master",
                table: "mst_subunit_department",
                column: "SubunitId");

            migrationBuilder.CreateIndex(
                name: "IX_mst_unit_GroupId",
                schema: "master",
                table: "mst_unit",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_notification_queue_WorkflowTransactionId",
                schema: "workflow",
                table: "notification_queue",
                column: "WorkflowTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_offer_letter_CandidateId",
                schema: "recruitment",
                table: "offer_letter",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ot_authorization_EmployeeId",
                schema: "attendance",
                table: "ot_authorization",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ot_details_EmployeeId",
                schema: "payroll",
                table: "ot_details",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ot_details_PayrollDetailId",
                schema: "payroll",
                table: "ot_details",
                column: "PayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_partial_salary_payment_EmployeeId",
                schema: "payroll",
                table: "partial_salary_payment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_password_history_UserId",
                schema: "security",
                table: "password_history",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_payroll_adjustment_EmployeeId",
                schema: "payroll",
                table: "payroll_adjustment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_payroll_details_EmployeeId",
                schema: "payroll",
                table: "payroll_details",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_payroll_details_PayrollId",
                schema: "payroll",
                table: "payroll_details",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_payroll_exception_EmployeeId",
                schema: "payroll",
                table: "payroll_exception",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_payslip_EmployeeId",
                schema: "payroll",
                table: "payslip",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_payslip_PayrollDetailId",
                schema: "payroll",
                table: "payslip",
                column: "PayrollDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_pending_approval_WorkflowTransactionId",
                schema: "workflow",
                table: "pending_approval",
                column: "WorkflowTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_processed_attendance_EmployeeId",
                schema: "attendance",
                table: "processed_attendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_processed_attendance_ShiftId",
                schema: "attendance",
                table: "processed_attendance",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_raw_punch_DeviceId",
                schema: "attendance",
                table: "raw_punch",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_recruitment_workflow_CandidateId",
                schema: "recruitment",
                table: "recruitment_workflow",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_report_access_UserId",
                schema: "audit",
                table: "report_access",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_PermissionId",
                schema: "security",
                table: "role_permission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_RoleId",
                schema: "security",
                table: "role_permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_salary_structure_EmployeeId",
                schema: "payroll",
                table: "salary_structure",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_shift_roster_EmployeeId",
                schema: "attendance",
                table: "shift_roster",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_shift_roster_ShiftId",
                schema: "attendance",
                table: "shift_roster",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_special_payroll_band_PolicyId",
                schema: "payroll",
                table: "special_payroll_band",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_system_event_UserId",
                schema: "audit",
                table: "system_event",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tax_EmployeeId",
                schema: "payroll",
                table: "tax",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_thana_DistrictId",
                schema: "lookup",
                table: "thana",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_RoleId",
                schema: "security",
                table: "user_role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_UserId",
                schema: "security",
                table: "user_role",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_session_UserId",
                schema: "security",
                table: "user_session",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_weekly_off_pattern_EmployeeId",
                schema: "attendance",
                table: "weekly_off_pattern",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_attachment_WorkflowTransactionId",
                schema: "workflow",
                table: "workflow_attachment",
                column: "WorkflowTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_audit_WorkflowTransactionId",
                schema: "workflow",
                table: "workflow_audit",
                column: "WorkflowTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_step_WorkflowMasterId",
                schema: "workflow",
                table: "workflow_step",
                column: "WorkflowMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_workflow_transaction_WorkflowMasterId",
                schema: "workflow",
                table: "workflow_transaction",
                column: "WorkflowMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "approval_history",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "approval_trail",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "arrear",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "attendance_adjustment",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "attendance_exception",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "attendance_lock",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "bank_transfer",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "bonus",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "candidate_document",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "data_change",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "deduction",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "delegation",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "device_sync_log",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "emergency_access",
                schema: "security");

            migrationBuilder.DropTable(
                name: "employee_bank_account",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_contact",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_document",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_education",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_employment",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_experience",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_family",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_nature",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "employee_nominee",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_payroll",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_personal",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_reporting",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_training",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "employee_verification",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "escalation_rule",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "export_history",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "field_security",
                schema: "security");

            migrationBuilder.DropTable(
                name: "holiday_calendar",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "holiday_calendar",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "increment_history",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "inside_factory_status",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "interview_evaluation",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "leave_accrual",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_adjustment",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_application_details",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_approval_history",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_balance",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_cancellation",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_encashment",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_opening_balance",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_policy",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "leave_year",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "loan_recovery",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "login_history",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "manpower_requisition",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "medical_fitness_check",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "module_access",
                schema: "security");

            migrationBuilder.DropTable(
                name: "mst_department_section",
                schema: "master");

            migrationBuilder.DropTable(
                name: "mst_subunit_department",
                schema: "master");

            migrationBuilder.DropTable(
                name: "notification_queue",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "offer_letter",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "ot_authorization",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "ot_details",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "partial_salary_payment",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "password_history",
                schema: "security");

            migrationBuilder.DropTable(
                name: "payroll_adjustment",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "payroll_exception",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "payroll_lock",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "payroll_process_log",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "payslip",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "pending_approval",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "physical_examination_setting",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "processed_attendance",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "processing_log",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "raw_punch",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "recruitment_workflow",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "report_access",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "role_permission",
                schema: "security");

            migrationBuilder.DropTable(
                name: "salary_structure",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "shift_roster",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "special_payroll_band",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "system_event",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "tax",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "thana",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "user_role",
                schema: "security");

            migrationBuilder.DropTable(
                name: "user_session",
                schema: "security");

            migrationBuilder.DropTable(
                name: "weekly_off_pattern",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "workflow_attachment",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "workflow_audit",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "workflow_step",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "mst_cell",
                schema: "master");

            migrationBuilder.DropTable(
                name: "mst_designation",
                schema: "master");

            migrationBuilder.DropTable(
                name: "mst_employee_category",
                schema: "master");

            migrationBuilder.DropTable(
                name: "interview_schedule",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "leave_application",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "blood_group",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "mst_department",
                schema: "master");

            migrationBuilder.DropTable(
                name: "mst_subunit",
                schema: "master");

            migrationBuilder.DropTable(
                name: "payroll_details",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "device_master",
                schema: "attendance");

            migrationBuilder.DropTable(
                name: "permission",
                schema: "security");

            migrationBuilder.DropTable(
                name: "mst_shift",
                schema: "master");

            migrationBuilder.DropTable(
                name: "special_payroll_policy",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "role",
                schema: "security");

            migrationBuilder.DropTable(
                name: "user_account",
                schema: "security");

            migrationBuilder.DropTable(
                name: "workflow_transaction",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "mst_section",
                schema: "master");

            migrationBuilder.DropTable(
                name: "mst_grade",
                schema: "master");

            migrationBuilder.DropTable(
                name: "candidate",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "interview_panel",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "leave_type",
                schema: "leave_mgmt");

            migrationBuilder.DropTable(
                name: "district",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "employee_master",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "payroll_header",
                schema: "payroll");

            migrationBuilder.DropTable(
                name: "mst_unit",
                schema: "master");

            migrationBuilder.DropTable(
                name: "workflow_master",
                schema: "workflow");

            migrationBuilder.DropTable(
                name: "job_position",
                schema: "recruitment");

            migrationBuilder.DropTable(
                name: "division",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "payroll_processing_group",
                schema: "master");

            migrationBuilder.DropTable(
                name: "mst_group",
                schema: "master");
        }
    }
}
