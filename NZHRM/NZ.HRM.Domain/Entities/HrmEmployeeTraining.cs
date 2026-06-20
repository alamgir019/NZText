using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_training", Schema = "hrm")]
    public class HrmEmployeeTraining : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? TrainingName { get; set; }
        public string? TrainingProvider { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? TrainingHours { get; set; }
        public bool CertificateReceived { get; set; }
        public string? CertificateNo { get; set; }
        public string? Remarks { get; set; }
        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }
    }
}
