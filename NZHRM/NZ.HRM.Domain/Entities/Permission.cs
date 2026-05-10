namespace NZ.HRM.Domain.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public string Module { get; set; } = string.Empty;
        public string AccessType { get; set; } = string.Empty;

        public Role? Role { get; set; }
    }
}
