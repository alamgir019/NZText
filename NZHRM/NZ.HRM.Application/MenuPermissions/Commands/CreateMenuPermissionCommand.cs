public class CreateMenuPermissionCommand
{
    public string MenuId { get; set; } = string.Empty;
    public string? RoleId { get; set; }
    public string? UserId { get; set; }
    public string Permissions { get; set; } = "{\"Read\": true, \"Write\": true, \"Delete\": true}";
    public bool Visibility { get; set; } = true;
}
