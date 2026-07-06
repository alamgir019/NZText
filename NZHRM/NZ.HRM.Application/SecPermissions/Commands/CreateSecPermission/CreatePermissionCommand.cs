using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.Permissions.Commands.CreatePermission;

public class CreatePermissionCommand
{
    [Required]
    [MaxLength(100)]
    public string PermissionCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string PermissionName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? ModuleName { get; set; }

    [MaxLength(50)]
    public string? PermissionType { get; set; }
}
