using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;

namespace NZ.Leave.Infrastructure.Persistence;

public class LeaveDbContext : DbContext
{
    public LeaveDbContext(DbContextOptions<LeaveDbContext> options) : base(options) { }

    public DbSet<LevLeaveType> LevLeaveTypes => Set<LevLeaveType>();
    public DbSet<LevLeaveBalance> LevLeaveBalances => Set<LevLeaveBalance>();
    public DbSet<LevLeaveApplication> LevLeaveApplications => Set<LevLeaveApplication>();
    public DbSet<LevLeaveApplicationDetails> LevLeaveApplicationDetails => Set<LevLeaveApplicationDetails>();
    public DbSet<LevLeaveAdjustment> LevLeaveAdjustments => Set<LevLeaveAdjustment>();
    public DbSet<LevLeaveOpeningBalance> LevLeaveOpeningBalances => Set<LevLeaveOpeningBalance>();
    public DbSet<LevLeaveEncashment> LevLeaveEncashments => Set<LevLeaveEncashment>();
    public DbSet<LevLeaveAccrual> LevLeaveAccruals => Set<LevLeaveAccrual>();
    public DbSet<LevHolidayCalendar> LevHolidayCalendars => Set<LevHolidayCalendar>();
    public DbSet<LevLeaveApprovalHistory> LevLeaveApprovalHistories => Set<LevLeaveApprovalHistory>();
    public DbSet<LevLeaveCancellation> LevLeaveCancellations => Set<LevLeaveCancellation>();
    public DbSet<LevLeaveYear> LevLeaveYears => Set<LevLeaveYear>();
    public DbSet<LevLeavePolicy> LevLeavePolicies => Set<LevLeavePolicy>();

    // Cross-module read-only reference (owned by HRM module)
    public DbSet<HrmEmployeeMaster> HrmEmployeeMasters => Set<HrmEmployeeMaster>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<HrmEmployeeReporting>()
			.HasOne(r => r.ReportingEmployee)
			.WithMany(e => e.Reportings)
			.HasForeignKey(r => r.ReportingEmployeeId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<HrmEmployeeReporting>()
			.HasOne(r => r.Employee)
			.WithMany()
			.HasForeignKey(r => r.EmployeeId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
