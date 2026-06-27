using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_salary_account", Schema = "hrm")]
    public class HrmEmployeeSalaryAccount : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty; // e.g., "Bank Transfer", "Cash", etc.
        public string? BankingId { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNo { get; set; }
        public string? RoutingNo { get; set; }
        public string? BranchName { get; set; }
        public bool SalaryAccountFlag { get; set; }
        public string? AccountType { get; set; } // e.g., "Savings", "Current", etc.

        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }

        [ForeignKey("BankingId")]
        public LookBanking? Banking { get; set; }
    }
}
