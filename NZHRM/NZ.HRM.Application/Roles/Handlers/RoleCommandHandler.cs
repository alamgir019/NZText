using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Helper;

public class RoleCommandHandler
{
    private readonly IRoleRepository _repo;
    public RoleCommandHandler(IRoleRepository repo) => _repo = repo;

    public async Task<string> Handle(CreateRoleCommand cmd)
    {
        var role = new SecRole
        {
            RoleName = cmd.RoleName,
            CreatedBy = cmd.CreatedBy,
            UpdatedBy = cmd.CreatedBy,
            IsActive = true
        };
        await _repo.AddAsync(role);
        await _repo.SaveChangesAsync();
        return role.Id;
    }

    public async Task Handle(UpdateRoleCommand cmd)
    {
        var role = await _repo.FindByIdAsync(cmd.Id);
        if (role is null) throw new Exception("Role not found");
        role.RoleName = cmd.RoleName;
        role.UpdatedOn = DateTime.UtcNow;
        role.UpdatedBy = cmd.UpdatedBy;
        role.IsActive = cmd.IsActive;
        await _repo.UpdateAsync(role);
        await _repo.SaveChangesAsync();
    }

    public async Task Handle(DeleteRoleCommand cmd)
    {
        var role = await _repo.FindByIdAsync(cmd.Id);
        if (role is null) throw new Exception("Role not found");
        await _repo.RemoveAsync(role);
        await _repo.SaveChangesAsync();
    }
}
