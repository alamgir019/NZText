using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;

namespace NZ.Payroll.Infrastructure.Persistence;

public class PayrollDbContext : DbContext
{
    public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options) { }

    public DbSet<PaySalaryStructure> PaySalaryStructures => Set<PaySalaryStructure>();
    public DbSet<PayIncrementHistory> PayIncrementHistories => Set<PayIncrementHistory>();
    public DbSet<PayPayrollHeader> PayPayrollHeaders => Set<PayPayrollHeader>();
    public DbSet<PayPayrollDetails> PayPayrollDetails => Set<PayPayrollDetails>();
    public DbSet<PayOtDetails> PayOtDetails => Set<PayOtDetails>();
    public DbSet<PayDeduction> PayDeductions => Set<PayDeduction>();
    public DbSet<PayArrear> PayArrears => Set<PayArrear>();
    public DbSet<PayBonus> PayBonuses => Set<PayBonus>();
    public DbSet<PayTax> PayTaxes => Set<PayTax>();
    public DbSet<PayLoanRecovery> PayLoanRecoveries => Set<PayLoanRecovery>();
    public DbSet<PayBankTransfer> PayBankTransfers => Set<PayBankTransfer>();
    public DbSet<PayPayslip> PayPayslips => Set<PayPayslip>();
    public DbSet<PayPayrollAdjustment> PayPayrollAdjustments => Set<PayPayrollAdjustment>();
    public DbSet<PayPayrollLock> PayPayrollLocks => Set<PayPayrollLock>();
    public DbSet<PayPayrollProcessLog> PayPayrollProcessLogs => Set<PayPayrollProcessLog>();
    public DbSet<PayPartialSalaryPayment> PayPartialSalaryPayments => Set<PayPartialSalaryPayment>();
    public DbSet<PaySpecialPayrollPolicy> PaySpecialPayrollPolicies => Set<PaySpecialPayrollPolicy>();
    public DbSet<PaySpecialPayrollBand> PaySpecialPayrollBands => Set<PaySpecialPayrollBand>();
    public DbSet<PayPayrollException> PayPayrollExceptions => Set<PayPayrollException>();

    // Cross-module read-only references
    public DbSet<HrmEmployeeMaster> HrmEmployeeMasters => Set<HrmEmployeeMaster>();
    public DbSet<HrmEmployeePayroll> HrmEmployeePayrolls => Set<HrmEmployeePayroll>();
    public DbSet<MstPayrollProcessingGroup> MstPayrollProcessingGroups => Set<MstPayrollProcessingGroup>();
    public DbSet<MstGrade> MstGrades => Set<MstGrade>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

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
    }
}
