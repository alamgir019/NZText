namespace NZ.HRM.Application.UserRoles.Queries.GetUserRoleById;

public class UserRoleDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
    public DateOnly? EffectiveDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }
}
