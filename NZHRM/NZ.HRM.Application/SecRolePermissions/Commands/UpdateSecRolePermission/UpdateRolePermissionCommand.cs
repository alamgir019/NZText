using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.RolePermissions.Commands.UpdateRolePermission;

public class UpdateRolePermissionCommand
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    public string RoleId { get; set; } = string.Empty;

    [Required]
    public string PermissionId { get; set; } = string.Empty;
}
