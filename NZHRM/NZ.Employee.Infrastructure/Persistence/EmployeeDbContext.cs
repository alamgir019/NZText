using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;

namespace NZ.Employee.Infrastructure.Persistence;

/// <summary>
/// Employee module DbContext — owns Employee Profile, Employment History, Document Management.
/// Architecture: Employee Service
/// </summary>
public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

    public DbSet<HrmEmployeeMaster> HrmEmployeeMasters => Set<HrmEmployeeMaster>();
    public DbSet<HrmEmployeePersonal> HrmEmployeePersonals => Set<HrmEmployeePersonal>();
    public DbSet<HrmEmployeeEmployment> HrmEmployeeEmployments => Set<HrmEmployeeEmployment>();
    public DbSet<HrmEmployeePayroll> HrmEmployeePayrolls => Set<HrmEmployeePayroll>();
    public DbSet<HrmEmployeeDocument> HrmEmployeeDocuments => Set<HrmEmployeeDocument>();
    public DbSet<HrmEmployeeNominee> HrmEmployeeNominees => Set<HrmEmployeeNominee>();
    public DbSet<HrmEmployeeEducation> HrmEmployeeEducations => Set<HrmEmployeeEducation>();
    public DbSet<HrmEmployeeExperience> HrmEmployeeExperiences => Set<HrmEmployeeExperience>();
    public DbSet<HrmEmployeeTraining> HrmEmployeeTrainings => Set<HrmEmployeeTraining>();
    public DbSet<HrmEmployeeFamily> HrmEmployeeFamilies => Set<HrmEmployeeFamily>();
    public DbSet<HrmEmployeeSalaryAccount> HrmEmployeeBankAccounts => Set<HrmEmployeeSalaryAccount>();
    public DbSet<HrmEmployeeReporting> HrmEmployeeReportings => Set<HrmEmployeeReporting>();
    public DbSet<HrmEmployeeVerification> HrmEmployeeVerifications => Set<HrmEmployeeVerification>();
    public DbSet<HrmMedicalFitnessCheck> HrmMedicalFitnessChecks => Set<HrmMedicalFitnessCheck>();
    public DbSet<HrmPhysicalExaminationSetting> HrmPhysicalExaminationSettings => Set<HrmPhysicalExaminationSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HrmEmployeeMaster>().ToTable("employee_master", "hrm");
        modelBuilder.Entity<HrmEmployeePersonal>().ToTable("employee_personal", "hrm")
            .Property(p => p.GuardianType).HasConversion<string>().HasMaxLength(50);
        modelBuilder.Entity<HrmEmployeeEmployment>().ToTable("employee_employment", "hrm");
        modelBuilder.Entity<HrmEmployeePayroll>().ToTable("employee_payroll", "hrm");
        modelBuilder.Entity<HrmEmployeeDocument>().ToTable("employee_document", "hrm");
        modelBuilder.Entity<HrmEmployeeNominee>().ToTable("employee_nominee", "hrm");
        modelBuilder.Entity<HrmEmployeeEducation>().ToTable("employee_education", "hrm");
        modelBuilder.Entity<HrmEmployeeExperience>().ToTable("employee_experience", "hrm");
        modelBuilder.Entity<HrmEmployeeTraining>().ToTable("employee_training", "hrm");
        modelBuilder.Entity<HrmEmployeeFamily>().ToTable("employee_family", "hrm");
        modelBuilder.Entity<HrmEmployeeSalaryAccount>().ToTable("employee_salary_account", "hrm");
        modelBuilder.Entity<HrmEmployeeReporting>().ToTable("employee_reporting", "hrm");
        modelBuilder.Entity<HrmEmployeeVerification>().ToTable("employee_verification", "hrm");
        modelBuilder.Entity<HrmMedicalFitnessCheck>().ToTable("medical_fitness_check", "hrm");
        modelBuilder.Entity<HrmPhysicalExaminationSetting>().ToTable("physical_examination_setting", "hrm");

        modelBuilder.Entity<HrmEmployeeMaster>()
            .HasOne(e => e.Personal).WithOne(p => p.Employee)
            .HasForeignKey<HrmEmployeePersonal>(p => p.EmployeeId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HrmEmployeeMaster>()
            .HasOne(e => e.Employment).WithOne(emp => emp.Employee)
            .HasForeignKey<HrmEmployeeEmployment>(emp => emp.EmployeeId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HrmEmployeeMaster>()
            .HasOne(e => e.Payroll).WithOne(p => p.Employee)
            .HasForeignKey<HrmEmployeePayroll>(p => p.EmployeeId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HrmEmployeeReporting>()
            .HasOne(r => r.ReportingEmployee).WithMany(e => e.Reportings)
            .HasForeignKey(r => r.ReportingEmployeeId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HrmEmployeeReporting>()
            .HasOne(r => r.Employee).WithMany()
            .HasForeignKey(r => r.EmployeeId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<HrmEmployeePayroll>()
            .HasOne(p => p.SalaryAccount).WithMany()
            .HasForeignKey(p => p.SalaryAccountId).OnDelete(DeleteBehavior.Restrict);
    }
}
