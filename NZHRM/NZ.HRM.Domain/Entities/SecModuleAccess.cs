using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("module_access", Schema = "security")]
    public class SecModuleAccess : BaseEntity
    {
        public string RoleId { get; set; } = string.Empty;
        public string ModuleCode { get; set; } = string.Empty;
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanApprove { get; set; }
        public bool CanExport { get; set; }

        [ForeignKey(nameof(RoleId))] public SecRole? Role { get; set; }
    }
}
