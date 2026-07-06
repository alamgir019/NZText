using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.RolePermissions.Commands.CreateRolePermission;

public class CreateRolePermissionCommand
{
    [Required]
    public string RoleId { get; set; } = string.Empty;

    [Required]
    public string PermissionId { get; set; } = string.Empty;
}
