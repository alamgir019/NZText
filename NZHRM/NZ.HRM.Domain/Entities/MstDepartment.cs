using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_department", Schema = "master")]
    public class MstDepartment : BaseEntityWithSortOrder
    {
        public string SubunitId { get; set; } = string.Empty;
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        // Navigation
        public ICollection<MstSubunitDepartment> SubunitDepartments { get; set; } = new HashSet<MstSubunitDepartment>();
        public ICollection<MstDepartmentSection> DepartmentSections { get; set; } = new HashSet<MstDepartmentSection>();
    }
}
