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
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Requisition> Requisitions => Set<Requisition>();
        public DbSet<ApplicationTracking> ApplicationTrackings => Set<ApplicationTracking>();
        public DbSet<OfferLetter> OfferLetters => Set<OfferLetter>();
        public DbSet<District> Districts => Set<District>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<MenuPermission>().ToTable("MenuPermissions");
            modelBuilder.Entity<ApplicationTracking>().ToTable("ApplicationTrackings");
            modelBuilder.Entity<OfferLetter>().ToTable("OfferLetters");
            modelBuilder.Entity<District>().ToTable("Districts");
            modelBuilder.Entity<Location>().ToTable("Locations");

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
