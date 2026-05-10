using NZ.HRM.Domain.Entities;

public class MenuPermissionQueryHandler
{
    private readonly IMenuPermissionRepository _repo;
    private readonly IUserRepository _userRepo;

    public MenuPermissionQueryHandler(IMenuPermissionRepository repo, IUserRepository userRepo)
    {
        _repo = repo;
        _userRepo = userRepo;
    }

    public async Task<List<Menu>> GetMenusByUserIdAsync(string userId)
    {
        var userMenus = await _repo.GetByUserIdAsync(userId);
        if (userMenus.Any())
            return userMenus.Select(mp => mp.Menu!).ToList();

        var user = await _userRepo.FindByIdAsync(userId);
        if (user == null || string.IsNullOrEmpty(user.RoleId))
            return new List<Menu>();

        var roleMenus = await _repo.GetByRoleIdAsync(user.RoleId);
        return roleMenus.Select(mp => mp.Menu!).ToList();
    }
}
