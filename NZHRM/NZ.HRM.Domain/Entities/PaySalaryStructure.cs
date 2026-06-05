using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("salary_structure", Schema = "payroll")]
    public class PaySalaryStructure : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public DateOnly? EffectiveDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseRent { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal ConveyanceAllowance { get; set; }
        public decimal FoodAllowance { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal GrossSalary { get; set; }
        public bool ActiveFlag { get; set; }
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
    }
}
