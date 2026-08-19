using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;
using NZ.Shared.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_department", Schema = "master")]
    public class MstDepartment : BaseEntityWithSortOrder
    {
        public string DepartmentCode { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string? DepartmentNameBangla { get; set; }

        // Navigation
        public ICollection<MstDepartmentUnitComplex> DepartmentUnitComplexes { get; set; } = new HashSet<MstDepartmentUnitComplex>();
        public ICollection<MstSection> Sections { get; set; } = new HashSet<MstSection>();
    }
}
