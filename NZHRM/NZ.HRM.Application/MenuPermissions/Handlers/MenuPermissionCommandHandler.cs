using NZ.HRM.Domain.Entities;
using System.Threading.Tasks;

public class MenuPermissionCommandHandler
{
    private readonly IMenuPermissionRepository _repo;
    public MenuPermissionCommandHandler(IMenuPermissionRepository repo) => _repo = repo;

    public async Task<string> Handle(CreateMenuPermissionCommand cmd)
    {
        //var menuPermission = new MenuPermission
        //{
        //    MenuId = cmd.MenuId,
        //    RoleId = cmd.RoleId,
        //    UserId = cmd.UserId,
        //    Permissions = cmd.Permissions,
        //    Visibility = cmd.Visibility
        //};
        //await _repo.AddAsync(menuPermission);
        //await _repo.SaveChangesAsync();
        //return menuPermission.Id;
        return string.Empty;
    }
}
