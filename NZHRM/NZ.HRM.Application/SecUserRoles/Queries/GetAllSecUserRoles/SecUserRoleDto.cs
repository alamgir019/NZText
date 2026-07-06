using System;

namespace NZ.HRM.Application.UserRoles.Queries.GetAllUserRoles;

public class UserRoleDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
    public DateOnly? EffectiveDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
}
