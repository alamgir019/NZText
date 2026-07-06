using NZ.HRM.Application.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Helper;

public class UserCommandHandler
{
    private readonly IUserRepository _repo;
    public UserCommandHandler(IUserRepository repo) => _repo = repo;

    public async Task<string> Handle(CreateUserCommand cmd)
    {
        var user = new SecUser
        {
            Id = IdentityGenerator.Next(),
            UserName = cmd.Username,
            PasswordHash = cmd.Password, // Hash in production!
            EmployeeId = cmd.EmployeeId,
            CreatedBy = cmd.CreatedBy,
            UpdatedBy = cmd.CreatedBy,
            IsActive = true
        };

        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();
        return user.Id;
    }

    public async Task Handle(UpdateUserCommand cmd)
    {
        var user = await _repo.FindByIdAsync(cmd.Id);
        if (user is null) throw new Exception("User not found");
        user.UserName = cmd.Username;
        user.PasswordHash = cmd.Password; // Hash in production!
        user.EmployeeId = cmd.EmployeeId;
        user.UpdatedOn = DateTime.UtcNow;
        user.UpdatedBy = cmd.UpdatedBy;
        user.IsActive = cmd.IsActive;
        await _repo.UpdateAsync(user);
        await _repo.SaveChangesAsync();
    }

    public async Task Handle(DeleteUserCommand cmd)
    {
        var user = await _repo.FindByIdAsync(cmd.Id);
        if (user is null) throw new Exception("User not found");
        await _repo.RemoveAsync(user);
        await _repo.SaveChangesAsync();
    }

    public async Task<LoginUserDto?> Handle(LoginUserCommand cmd)
    {
        var user = await _repo.FindByUsernameAsync(cmd.Username);
        if (user == null) return null;
        // In production, use hashed and salted password comparison!
        if (user.PasswordHash != cmd.Password) return null;
        return new LoginUserDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            EmployeeId = user.EmployeeId,
            UnitId = user.EmployeeMaster?.Employment?.UnitId,
            PermissionNames = user.UserRoles.SelectMany(ur => ur.Role.RolePermissions).Select(rp => rp.Permission.PermissionName).ToList(),
            RoleNames = user.UserRoles.Select(ur => ur.Role.RoleName).ToList(),
            ModuleNames = user.UserRoles.SelectMany(ur => ur.Role.RolePermissions).Select(rp => rp.Permission.ModuleName).ToList()
        };
    }

}
