using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;

namespace NZ.Organization.Infrastructure.Persistence;

/// <summary>
/// Organization module DbContext — owns Department, Designation, Reporting Structure, Location.
/// Architecture: Organization Service
/// </summary>
public class OrganizationDbContext : DbContext
{
    public OrganizationDbContext(DbContextOptions<OrganizationDbContext> options) : base(options) { }

    // Organizational structure
    public DbSet<MstGroup> MstGroups => Set<MstGroup>();
    public DbSet<MstGroupComplex> MstGroupComplexes => Set<MstGroupComplex>();
    public DbSet<MstUnit> MstUnits => Set<MstUnit>();
    public DbSet<MstSubunit> MstSubunits => Set<MstSubunit>();
    public DbSet<MstDepartment> MstDepartments => Set<MstDepartment>();
    public DbSet<MstSection> MstSections => Set<MstSection>();
    public DbSet<MstCell> MstCells => Set<MstCell>();
    public DbSet<MstDesignation> MstDesignations => Set<MstDesignation>();
    public DbSet<MstGrade> MstGrades => Set<MstGrade>();
    public DbSet<MstShift> MstShifts => Set<MstShift>();
    public DbSet<MstPayrollProcessingGroup> MstPayrollProcessingGroups => Set<MstPayrollProcessingGroup>();
    public DbSet<MstDepartmentUnitComplex> MstDepartmentUnitComplexes => Set<MstDepartmentUnitComplex>();

    // Reference / Lookup data
    public DbSet<LookDivision> Divisions => Set<LookDivision>();
    public DbSet<LookDistrict> Districts => Set<LookDistrict>();
    public DbSet<LookThana> Thanas => Set<LookThana>();
    public DbSet<LookBanking> Banks => Set<LookBanking>();
    public DbSet<LookKeyValue> LookupKeyValues => Set<LookKeyValue>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstGroup>().ToTable("mst_group", "master");
        modelBuilder.Entity<MstUnit>().ToTable("mst_unit", "master");
        modelBuilder.Entity<MstSubunit>().ToTable("mst_subunit", "master");
        modelBuilder.Entity<MstDepartment>().ToTable("mst_department", "master");
        modelBuilder.Entity<MstSection>().ToTable("mst_section", "master");
        modelBuilder.Entity<MstCell>().ToTable("mst_cell", "master");
        modelBuilder.Entity<MstDesignation>().ToTable("mst_designation", "master");
        modelBuilder.Entity<MstGrade>().ToTable("mst_grade", "master");
        modelBuilder.Entity<MstShift>().ToTable("mst_shift", "master");
        modelBuilder.Entity<MstPayrollProcessingGroup>().ToTable("payroll_processing_group", "master");
        modelBuilder.Entity<MstDepartmentUnitComplex>().ToTable("mst_department_unit_complex", "master");
        modelBuilder.Entity<MstGroupComplex>().ToTable("mst_group_complex", "master");
        modelBuilder.Entity<LookKeyValue>().ToTable("lookup_key_value", "lookup");
    }
}
