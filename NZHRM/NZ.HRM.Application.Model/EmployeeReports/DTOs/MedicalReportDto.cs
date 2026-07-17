namespace NZ.HRM.Application.Model.EmployeeReports.DTOs;

public class MedicalReportDto
{
    public string SlipNo { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string EnrollmentId { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string MotherName { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string BloodGroup { get; set; } = string.Empty;
    public string Village { get; set; } = string.Empty;
    public string PostOffice { get; set; } = string.Empty;
    public string PoliceStation { get; set; } = string.Empty;
    public string Upazila { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Height { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public string TestedBloodGroup { get; set; } = string.Empty;
    public string IdentificationMark { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public string DoctorQualification { get; set; } = string.Empty;
    public bool IsFit { get; set; }
    public string Remarks { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
}
