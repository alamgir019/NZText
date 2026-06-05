using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_documents", Schema = "hrm")]
    public class HrmEmployeeDocument : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? DocumentTypeId { get; set; }
        public string? DocumentNo { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? Remarks { get; set; }
        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }
    }
}
