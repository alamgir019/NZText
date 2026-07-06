using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Application.SecUserRoles.Commands.CreateUserRole;

public class CreateUserRoleCommand
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string RoleId { get; set; } = string.Empty;

    public DateOnly? EffectiveDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
}
