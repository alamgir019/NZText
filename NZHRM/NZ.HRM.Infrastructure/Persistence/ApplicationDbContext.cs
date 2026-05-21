using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Common;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<MenuPermission> MenuPermissions => Set<MenuPermission>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Designation> Designations => Set<Designation>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<CompanyLocation> CompanyLocations => Set<CompanyLocation>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Requisition> Requisitions => Set<Requisition>();
        public DbSet<ApplicationTracking> ApplicationTrackings => Set<ApplicationTracking>();
        public DbSet<OfferLetter> OfferLetters => Set<OfferLetter>();
        public DbSet<Division> Divisions => Set<Division>();
        public DbSet<District> Districts => Set<District>();
        public DbSet<Thana> Thanas => Set<Thana>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<DepartmentSection> DepartmentSections => Set<DepartmentSection>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<SectionCell> SectionCells => Set<SectionCell>();
        public DbSet<Cell> Cells => Set<Cell>();
        public DbSet<EmployeeMaster> EmployeeMasters => Set<EmployeeMaster>();
        public DbSet<EmployeePersonal> EmployeePersonals => Set<EmployeePersonal>();
        public DbSet<EmployeeVerification> EmployeeVerifications => Set<EmployeeVerification>();
        public DbSet<MedicalFitnessCheck> MedicalFitnessChecks => Set<MedicalFitnessCheck>();
        public DbSet<PhysicalExaminationSetting> PhysicalExaminationSettings => Set<PhysicalExaminationSetting>();
        public DbSet<Shift> Shifts => Set<Shift>();
        public DbSet<Holiday> Holidays => Set<Holiday>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<MenuPermission>().ToTable("MenuPermissions");
            modelBuilder.Entity<ApplicationTracking>().ToTable("ApplicationTrackings");
            modelBuilder.Entity<OfferLetter>().ToTable("OfferLetters");
            modelBuilder.Entity<Division>().ToTable("Divisions");
            modelBuilder.Entity<District>().ToTable("Districts");
            modelBuilder.Entity<Thana>().ToTable("Thanas");
            modelBuilder.Entity<District>().ToTable("Districts");
            modelBuilder.Entity<Location>().ToTable("Locations");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Grade>().ToTable("Grades");
            modelBuilder.Entity<Section>().ToTable("Sections");
            modelBuilder.Entity<Cell>().ToTable("Cells");
            modelBuilder.Entity<EmployeeMaster>().ToTable("EmployeeMasters");
            modelBuilder.Entity<EmployeePersonal>().ToTable("EmployeePersonals");
            modelBuilder.Entity<EmployeeVerification>().ToTable("EmployeeVerifications");
            modelBuilder.Entity<MedicalFitnessCheck>().ToTable("MedicalFitnessChecks");
            modelBuilder.Entity<PhysicalExaminationSetting>().ToTable("PhysicalExaminationSettings");
            modelBuilder.Entity<Shift>().ToTable("Shifts");
            modelBuilder.Entity<Holiday>().ToTable("Holidays");


            // Apply to all entities that have CreatedOn and UpdatedOn properties
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Configure CreatedOn
                var createdOnProperty = entityType.FindProperty("CreatedOn");
                if (createdOnProperty != null && createdOnProperty.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedOn")
                        .HasDefaultValueSql("NOW()");
                }

                // Configure UpdatedOn
                var updatedOnProperty = entityType.FindProperty("UpdatedOn");
                if (updatedOnProperty != null && updatedOnProperty.ClrType == typeof(DateTime))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("UpdatedOn")
                        .HasDefaultValueSql("NOW()");
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
