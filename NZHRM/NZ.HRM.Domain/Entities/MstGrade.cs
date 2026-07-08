using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_grade", Schema = "master")]
    public class MstGrade : BaseEntityWithSortOrder
    {
        [MaxLength(50)]
        public string GradeCode { get; set; } = string.Empty;
        [MaxLength(200)]
        public string GradeName { get; set; } = string.Empty;
        public decimal MinimumSalary { get; set; }
        public decimal MaximumSalary { get; set; }
    }
}
