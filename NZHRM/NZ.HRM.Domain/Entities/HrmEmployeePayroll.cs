using System;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_payroll", Schema = "hrm")]
    public class HrmEmployeePayroll : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty; // FK to employee_master.Id

        public decimal? ProposedSalary { get; set; }
        public decimal? GrossSalary { get; set; }
        public decimal? BankPortion { get; set; }
        public decimal? CashPortion { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? HouseRentAllowance { get; set; }
        public decimal? ConveyanceAllowance { get; set; }
        public decimal? MedicalAllowance { get; set; }
        public decimal? FoodAllowance { get; set; }
        public string? OtherAllowance { get; set; }
        public string? SalaryAccountId { get; set; }
        public string? TINNo { get; set; }
        public decimal? Tax { get; set; }

        [ForeignKey("EmployeeId")] public HrmEmployeeMaster? Employee { get; set; }
        [ForeignKey("SalaryAccountId")] public HrmEmployeeSalaryAccount? SalaryAccount { get; set; }
    }
}
