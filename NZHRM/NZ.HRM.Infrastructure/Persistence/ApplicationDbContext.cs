using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<MenuPermission>().ToTable("MenuPermissions");
            base.OnModelCreating(modelBuilder);
        }
    }
}
