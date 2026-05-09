using System.ComponentModel.DataAnnotations;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeeMasters.Commands.UpdateEmployeeMaster;

public class UpdateEmployeeMasterCommand
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee code is required")]
    [MaxLength(50, ErrorMessage = "Employee code must not exceed 50 characters")]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee name (English) is required")]
    [MaxLength(100, ErrorMessage = "Employee name must not exceed 100 characters")]
    public string EmployeeNameEnglish { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "Employee name (Bangla) must not exceed 100 characters")]
    public string? EmployeeNameBangla { get; set; }

    [Required(ErrorMessage = "Company ID is required")]
    public string CompanyId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department ID is required")]
    public string DepartmentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Section ID is required")]
    public string SectionId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Grade ID is required")]
    public string GradeId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee type is required")]
    public EmployeeType EmployeeType { get; set; }

    [Required(ErrorMessage = "Shift is required")]
    public Shift Shift { get; set; }

    [Required(ErrorMessage = "Employee nature is required")]
    public EmployeeNature EmployeeNature { get; set; }

    public Holiday Holiday { get; set; }

    public decimal? ProposedMonthlySalary { get; set; }

    [Required(ErrorMessage = "Joining date is required")]
    public DateTime JoiningDate { get; set; }

    public DateTime? ConfirmationDate { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public EmployeeStatus Status { get; set; }
}
