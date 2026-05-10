using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
