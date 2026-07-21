using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("mst_designation", Schema = "master")]
    public class MstDesignation : BaseEntityWithSortOrder
    {
        public string DesignationCode { get; set; } = string.Empty;
        public string DesignationName { get; set; } = string.Empty;
        public string? DesignationNameBangla { get; set; }
        public string? EmployeeNature { get; set; }
        public bool OtEligible { get; set; }
    }
}
