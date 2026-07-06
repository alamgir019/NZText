namespace NZ.HRM.Application.Permissions.Queries.GetAllSecPermissions;

public class SecPermissionDto
{
    public string Id { get; set; } = string.Empty;
    public string PermissionCode { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public string? ModuleName { get; set; }
    public string? PermissionType { get; set; }
}
