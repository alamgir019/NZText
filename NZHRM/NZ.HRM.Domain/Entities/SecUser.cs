using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("user_account", Schema = "security")]
    public class SecUser : BaseEntity
    {
        public string? EmployeeId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public DateTime? LastLoginDate { get; set; }

        [ForeignKey(nameof(EmployeeId))] public HrmEmployeeMaster? EmployeeMaster { get; set; }
        public ICollection<SecUserRole> UserRoles { get; set; } = new HashSet<SecUserRole>();
        public ICollection<SecUserSession> Sessions { get; set; } = new HashSet<SecUserSession>();
    }
}
