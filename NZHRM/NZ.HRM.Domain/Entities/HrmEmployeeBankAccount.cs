using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NZ.HRM.Domain.Common;

namespace NZ.HRM.Domain.Entities
{
    [Table("employee_bank_account", Schema = "hrm")]
    public class HrmEmployeeBankAccount : BaseEntityWithSortOrder
    {
        public string EmployeeId { get; set; } = string.Empty;
        public string? BankId { get; set; }
        public string? AccountName { get; set; }
        public string? AccountNo { get; set; }
        public string? RoutingNo { get; set; }
        public string? BranchName { get; set; }
        public bool MobileBankingFlag { get; set; }
        public bool SalaryAccountFlag { get; set; }
        [ForeignKey("EmployeeId")]
        public HrmEmployeeMaster? Employee { get; set; }
    }
}
