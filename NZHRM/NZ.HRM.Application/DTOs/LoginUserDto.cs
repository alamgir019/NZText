namespace NZ.HRM.Application.DTOs;

public class LoginUserDto
{
    //user.Id, user.UserName, user.EmployeeId,
    //        user.EmployeeMaster?.Employment?.UnitId, user.UserRoles.First().Role.RolePermissions.First().Permission.PermissionName}
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? EmployeeId { get; set; }
    public string? UnitId { get; set; }
    public List<string>? PermissionNames { get; set; }
    public List<string>? RoleNames { get; set; }
    public List<string>? ModuleNames { get; set; }
}