using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class MenuPermission : BaseEntity
    {
        public string MenuId { get; set; } = string.Empty;
        public Menu? Menu { get; set; }

        public string RoleId { get; set; } = string.Empty;
        public Role? Role { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public string Permissions { get; set; } = string.Empty; // e.g., "Read,Write,Delete"

        public bool Visibility { get; set; }
    }
}
