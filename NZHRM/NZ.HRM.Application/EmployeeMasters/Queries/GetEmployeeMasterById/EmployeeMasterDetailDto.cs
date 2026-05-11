using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeeMasters.Queries.GetEmployeeMasterById;

public class EmployeeMasterDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeNameEnglish { get; set; } = string.Empty;
    public string? EmployeeNameBangla { get; set; }
    public string CompanyId { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string SectionId { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public string GradeId { get; set; } = string.Empty;
    public string GradeName { get; set; } = string.Empty;
    public EmployeeType? EmployeeType { get; set; }
    public Shift? Shift { get; set; }
    public EmployeeNature? EmployeeNature { get; set; }
    public Holiday? Holiday { get; set; }
    public decimal? ProposedMonthlySalary { get; set; }
    public DateTime? JoiningDate { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public EmployeeStatus? Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedOn { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
