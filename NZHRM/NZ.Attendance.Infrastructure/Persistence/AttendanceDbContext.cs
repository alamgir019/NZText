using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;

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

    // Cross-module read-only references (owned by HRM module)
    public DbSet<HrmEmployeeMaster> HrmEmployeeMasters => Set<HrmEmployeeMaster>();
    public DbSet<MstShift> MstShifts => Set<MstShift>();
    public DbSet<LevHolidayCalendar> LevHolidayCalendars => Set<LevHolidayCalendar>();
}
