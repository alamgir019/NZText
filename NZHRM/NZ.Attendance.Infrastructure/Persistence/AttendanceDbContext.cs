using Microsoft.EntityFrameworkCore;
using NZ.Attendance.Domain.Entities;
using NZ.HRM.Domain.Entities;
using NZ.Leave.Domain.Entities;

namespace NZ.Attendance.Infrastructure.Persistence;

public class AttendanceDbContext : DbContext
{
    public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options) { }

    public DbSet<AttDeviceMaster> AttDeviceMasters => Set<AttDeviceMaster>();
    public DbSet<AttDeviceSyncLog> AttDeviceSyncLogs => Set<AttDeviceSyncLog>();
    public DbSet<AttRawPunch> AttRawPunches => Set<AttRawPunch>();
    public DbSet<AttProcessedPunch> AttProcessedPunches => Set<AttProcessedPunch>();
    public DbSet<AttShiftRoster> AttShiftRosters => Set<AttShiftRoster>();
    public DbSet<AttOtAuthorization> AttOtAuthorizations => Set<AttOtAuthorization>();
    public DbSet<AttProcessedAttendance> AttProcessedAttendances => Set<AttProcessedAttendance>();
    public DbSet<AttAttendanceException> AttAttendanceExceptions => Set<AttAttendanceException>();
    public DbSet<AttAttendanceAdjustment> AttAttendanceAdjustments => Set<AttAttendanceAdjustment>();
    public DbSet<AttAttendanceLock> AttAttendanceLocks => Set<AttAttendanceLock>();
    public DbSet<AttProcessingLog> AttProcessingLogs => Set<AttProcessingLog>();
    public DbSet<AttInsideFactoryStatus> AttInsideFactoryStatuses => Set<AttInsideFactoryStatus>();
    public DbSet<AttWeeklyOffPattern> AttWeeklyOffPatterns => Set<AttWeeklyOffPattern>();
    public DbSet<AttOtRequestItem> AttOtRequestItems => Set<AttOtRequestItem>();

    // Cross-module read-only references (owned by HRM module)
    public DbSet<HrmEmployeeMaster> HrmEmployeeMasters => Set<HrmEmployeeMaster>();
    public DbSet<MstShift> MstShifts => Set<MstShift>();
    public DbSet<LevHolidayCalendar> LevHolidayCalendars => Set<LevHolidayCalendar>();
    public DbSet<HrmEmployeeEmployment> HrmEmployeeEmployments => Set<HrmEmployeeEmployment>();
    public DbSet<MstDepartment> MstDepartments => Set<MstDepartment>();
    public DbSet<MstDesignation> MstDesignations => Set<MstDesignation>();
    // C#
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // HrmEmployeeMaster is only a read-only cross-module reference here.
    // Ignore navigations that would otherwise pull in unrelated HRM entity graphs
    // and cause ambiguous relationship errors (e.g., HrmEmployeeReporting has
    // two FKs to HrmEmployeeMaster: Employee/EmployeeId and ReportingEmployee/ReportingEmployeeId).
    modelBuilder.Entity<HrmEmployeeMaster>(entity =>
    {
        entity.Ignore(e => e.Personal);
        entity.Ignore(e => e.Employment);
        entity.Ignore(e => e.Payroll);
        entity.Ignore(e => e.Verification);
        entity.Ignore(e => e.MedicalFitnessCheck);
        entity.Ignore(e => e.Documents);
        entity.Ignore(e => e.Nominees);
        entity.Ignore(e => e.Educations);
        entity.Ignore(e => e.Experiences);
        entity.Ignore(e => e.Trainings);
        entity.Ignore(e => e.FamilyMembers);
        entity.Ignore(e => e.BankAccounts);
        entity.Ignore(e => e.Reportings);
    });

    // HrmEmployeeEmployment is also only a read-only cross-module reference here.
    // Ignore navigations to unrelated master entities not mapped in this DbContext.
    modelBuilder.Entity<HrmEmployeeEmployment>(entity =>
    {
        entity.Ignore(e => e.Employee);
        entity.Ignore(e => e.Group);
        entity.Ignore(e => e.Unit);
        entity.Ignore(e => e.Subunit);
        entity.Ignore(e => e.Section);
        entity.Ignore(e => e.Cell);
        entity.Ignore(e => e.Grade);
        entity.Ignore(e => e.Shift);
        entity.Ignore(e => e.ProcessingGroup);
    });
}
}
