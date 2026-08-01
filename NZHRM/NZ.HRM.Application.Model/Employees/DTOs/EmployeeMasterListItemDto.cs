namespace NZ.HRM.Application.Model.Employees.DTOs;

public class EmployeeMasterListItemDto
{
    /// <summary>
    /// Employee ID
    /// </summary>
    public string EmployeeId { get; set; } = string.Empty;

    /// <summary>
    /// Employee Code
    /// </summary>
    public string EmployeeCode { get; set; } = string.Empty;

    /// <summary>
    /// Employee Name
    /// </summary>
    public string EmployeeName { get; set; } = string.Empty;

    /// <summary>
    /// Department Name
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// Section Name
    /// </summary>
    public string SectionName { get; set; } = string.Empty;

    /// <summary>
    /// Cell Name
    /// </summary>
    public string CellName { get; set; } = string.Empty;

    /// <summary>
    /// Designation Name
    /// </summary>
    public string DesignationName { get; set; } = string.Empty;

    /// <summary>
    /// Employee Nature (Worker, Staff, Management, etc.)
    /// </summary>
    public string EmployeeNature { get; set; } = string.Empty;

    /// <summary>
    /// Joining Date
    /// </summary>
    public string? JoiningDate { get; set; }

    /// <summary>
    /// Active status
    /// </summary>
    public bool IsActive { get; set; }
    public string EnrollmentId { get; set; }
    public string IdentificationSign { get; set; }
    public string Fitness { get; set; }
    public DateTime? MedicalUpdatedOn { get; set; }
    public string Remarks { get; set; }
}
