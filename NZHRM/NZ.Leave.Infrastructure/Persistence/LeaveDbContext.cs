using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.Leave.Domain.Entities;

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
    public DbSet<LevLeaveEncashmentHistory> LevLeaveEncashmentHistories => Set<LevLeaveEncashmentHistory>();
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

        // HrmEmployeeMaster is only a read-only cross-module reference here.
        // Ignore navigations that would otherwise pull in unrelated HRM entity graphs
        // and cause ambiguous relationship errors (e.g., HrmEmployeeReporting has
        // two FKs to HrmEmployeeMaster: Employee/EmployeeId and ReportingEmployee/ReportingEmployeeId).
        modelBuilder.Entity<HrmEmployeeMaster>(entity =>
        {
            entity.Ignore(e => e.Personal);
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
        modelBuilder.Entity<HrmEmployeeEmployment>(entity =>
        {
            entity.Ignore(e => e.Cell);
            entity.Ignore(e => e.ProcessingGroup);
            entity.Ignore(e => e.Subunit);
            entity.Ignore(e => e.EmployeeCategory);
            entity.Ignore(e => e.Designation);
            entity.Ignore(e => e.Grade);
            entity.Ignore(e => e.Group);
            entity.Ignore(e => e.Section);
            entity.Ignore(e => e.Shift);
            entity.Ignore(e => e.Unit);
        });

        modelBuilder.Entity<HrmEmployeeMaster>()
            .HasOne(e => e.Employment)
            .WithOne(emp => emp.Employee)
            .HasForeignKey<HrmEmployeeEmployment>(emp => emp.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
