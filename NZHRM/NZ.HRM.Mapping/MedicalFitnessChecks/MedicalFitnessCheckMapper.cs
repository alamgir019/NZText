using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessReportByEmployeeId;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Mapping.MedicalFitnessChecks;

public static class MedicalFitnessCheckMapper
{
    public static MedicalFitnessReportDto MapToMedicalFitnessReportDto(this HrmEmployeeMaster employee, HrmMedicalFitnessCheck medical)
    {
        return new MedicalFitnessReportDto
        {
            EmployeeId = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            EnrollmentId = employee.EnrollmentId,
            //EmployeeName = employee.EmployeeNameEnglish,
            //FatherName = employee.PersonalInfo?.FatherNameEnglish,
            //MotherName = employee.PersonalInfo?.MotherNameEnglish,
            //DateOfBirth = employee.PersonalInfo?.DateOfBirth,
            //Gender = employee.PersonalInfo?.Gender,
            //MobileNumber = employee.PersonalInfo?.MobileNumber,
            //CompanyName = employee.Company?.CompanyName ?? string.Empty,
            //DepartmentName = employee.Department?.DepartmentName ?? string.Empty,
            //SectionName = employee.Section?.SectionName ?? string.Empty,
            //DesignationName = employee.Designation?.DesignationName ?? string.Empty,
            //PresentVillageAreaRoad = employee.PersonalInfo?.PresentVillageAreaRoad,
            //PresentPostOffice = employee.PersonalInfo?.PresentPostOffice,
            //PresentThana = employee.PersonalInfo?.PresentThana,
            //PresentDistrict = employee.PersonalInfo?.PresentDistrict,
            //PresentDivision = employee.PersonalInfo?.PresentDivision,
            MedicalFitnessCheckId = medical.Id,
            //BloodGroupTested = medical.BloodGroup,
            HeightCm = null,
            WeightKg = null,
            PhysicalExaminationDataJson = null,
            IsFit = string.Equals(medical.Fitness, "Fit", StringComparison.OrdinalIgnoreCase),
            Remarks = medical.Remarks,
            ExaminedByDoctor = medical.ExaminedByDoctor,
            ExaminationDateTime = medical.ExaminationDateTime
        };
    }
}
