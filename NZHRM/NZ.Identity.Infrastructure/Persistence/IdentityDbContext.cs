using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;

namespace NZ.Identity.Infrastructure.Persistence;

/// <summary>
/// Identity module DbContext — owns all security, auth, and role management entities.
/// Architecture: Identity Service (Auth, Authorization, Role Mgmt, MFA, SSO)
/// </summary>
public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    public DbSet<SecUser> SecUsers => Set<SecUser>();
    public DbSet<SecRole> SecRoles => Set<SecRole>();
    public DbSet<SecUserRole> SecUserRoles => Set<SecUserRole>();
    public DbSet<SecUserSession> SecUserSessions => Set<SecUserSession>();
    public DbSet<SecPermission> SecPermissions => Set<SecPermission>();
    public DbSet<SecRolePermission> SecRolePermissions => Set<SecRolePermission>();
    public DbSet<SecPasswordHistory> SecPasswordHistories => Set<SecPasswordHistory>();
    public DbSet<SecModuleAccess> SecModuleAccesses => Set<SecModuleAccess>();
    public DbSet<SecFieldSecurity> SecFieldSecurities => Set<SecFieldSecurity>();
    public DbSet<SecEmergencyAccess> SecEmergencyAccesses => Set<SecEmergencyAccess>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SecUser>().ToTable("user_account", "security");
        modelBuilder.Entity<SecRole>().ToTable("role", "security");
        modelBuilder.Entity<SecUserRole>().ToTable("user_role", "security");
        modelBuilder.Entity<SecUserSession>().ToTable("user_session", "security");
        modelBuilder.Entity<SecPermission>().ToTable("permission", "security")
            .Property(p => p.PermissionType).HasConversion<string>().HasMaxLength(50);
        modelBuilder.Entity<SecRolePermission>().ToTable("role_permission", "security");
        modelBuilder.Entity<SecPasswordHistory>().ToTable("password_history", "security");
        modelBuilder.Entity<SecModuleAccess>().ToTable("module_access", "security");
        modelBuilder.Entity<SecFieldSecurity>().ToTable("field_security", "security");
        modelBuilder.Entity<SecEmergencyAccess>().ToTable("emergency_access", "security");
    }
}
