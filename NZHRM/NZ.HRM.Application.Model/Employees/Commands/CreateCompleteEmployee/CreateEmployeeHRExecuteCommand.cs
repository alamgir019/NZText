using System.ComponentModel.DataAnnotations;
// file upload paths (stored by WebAPI controller)
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;

public class CreateEmployeeHRExecutiveCommand : CreateCandidateEntryCommand
{
    // Basic Employment Information
    [Required(ErrorMessage = "Employee ID is required")]
    public string EmployeeId { get; set; } = string.Empty;

    // Basic Employment Information
    public string? EmployeeName { get; set; }

    [Required(ErrorMessage = "Employee enrollment ID is required")]
    [MaxLength(50, ErrorMessage = "Employee enrollment ID must not exceed 50 characters")]
    public string EmployeeEnrollmentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subunit ID is required")]
    public string SubunitId { get; set; } = string.Empty;

    public string? DesignationId { get; set; }

    public string? GradeId { get; set; }

    public string? EmployeeTypeId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Shift is required")]
    public string ShiftId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee nature is required")]
    public EmployeeNature EmployeeNatureId { get; set; }

    [Required(ErrorMessage = "Employee code is required")]
    public string EmployeeCode { get; set; } = string.Empty;

    public WeekOffDay Holiday { get; set; }

    //salary information

    public decimal? BankPortion { get; set; }
    public decimal? CashPortion { get; set; }
    public Dictionary<string, decimal> OtherAllowance { get; set; } = new Dictionary<string, decimal>();
    public decimal? Tax { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // e.g., "Bank Transfer", "Cash", etc.
    public string? BankingId { get; set; }
    public string? AccountName { get; set; }
    public string? AccountNo { get; set; }
    public string? RoutingNo { get; set; }
    public string? BranchName { get; set; }
    public bool SalaryAccountFlag { get; set; }
    public string? AccountType { get; set; } // e.g., "Savings", "Current", etc.

    public List<EmployeeDocumentDto>? Documents { get; set; } = new List<EmployeeDocumentDto>();

    // Additional Information
    [MaxLength(50, ErrorMessage = "TIN number must not exceed 50 characters")]
    public string? TinNumber { get; set; }
    public decimal? ProbationPeriod { get; set; }
    public string? ReportingTo { get; set; }
    public string? ProcessingGroupId { get; set; }
    public decimal? GrossSalary { get; set; }
    public string? MotherName { get; set; }
    public string? FatherName { get; set; }
    public string? NomineeName { get; set; }
    public string? NomineeID { get; set; }
    public string? NomineeRelation { get; set; }
    public string? NomineeMobileNumber { get; set; }
    public string DepartmentId { get; set; } = string.Empty;
    public string SectionId { get; set; } = string.Empty;
    public string? CellId { get; set; }
}
