namespace NZ.HRM.Application.Model.EmployeeReports.DTOs;

public class JoiningLetterDto
{
    public string CurrentDate { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeNameBangla { get; set; } = string.Empty;
    public string FatherNameBangla { get; set; } = string.Empty;
    public string MotherNameBangla { get; set; } = string.Empty;
    public string? SpouseNameBangla { get; set; }
    public string PresentAddressBangla { get; set; } = string.Empty;
    public string PermanentAddressBangla { get; set; } = string.Empty;
    public string JoiningDate { get; set; } = string.Empty;
    public string? GradeBangla { get; set; }
    public string? DesignationBangla { get; set; }
    public string? DepartmentBangla { get; set; }
    public string? SectionBangla { get; set; }
    public string BasicSalary { get; set; } = string.Empty;
    public string HouseRent { get; set; } = string.Empty;
    public string MedicalAllowance { get; set; } = string.Empty;
    public string ConveyanceAllowance { get; set; } = string.Empty;
    public string FoodAllowance { get; set; } = string.Empty;
    public string GrossSalary { get; set; } = string.Empty;
}
