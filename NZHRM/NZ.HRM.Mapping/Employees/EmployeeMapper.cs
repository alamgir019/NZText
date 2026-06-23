using NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Mapping.Employees;

public static class EmployeeMapper
{
    public static HrmEmployeePersonal CreateCompleteEmployeeCommandToPersonal(CreateCompleteEmployeeCommand command, string employeeId)
    {
        return new HrmEmployeePersonal
        {
            EmployeeId = employeeId,
            DateOfBirth = command.DateOfBirth,
            Gender = command.Gender.ToString(),
            MaritalStatus = command.MaritalStatus.ToString(),
            //MobileNumber = command.MobileNumber,
            //EmailAddress = command.EmailAddress,
            //DocumentType = command.DocumentType.ToString(),
            //DocumentNumber = command.DocumentNumber,
            BloodGroup = command.BloodGroup.ToString(),
            Religion = command.Religion.ToString(),
            Nationality = command.Nationality.ToString(),
            //FatherNameEnglish = command.FatherNameEnglish,
            //FatherNameBangla = command.FatherNameBangla,
            //MotherNameEnglish = command.MotherNameEnglish,
            //MotherNameBangla = command.MotherNameBangla,
            SpouseName = command.SpouseName,
            //SpouseMobile = command.SpouseMobile,
            //IDNumber = command.TinNumber,
            //EmployeeReference = command.EmployeeReference,
            //ReferencePersonId = command.ReferencePersonId,
            //PermanentVillageAreaRoad = command.PermanentVillageAreaRoad,
            //PermanentPostOffice = command.PermanentPostOffice,
            //PermanentThana = command.PermanentThana,
            //PermanentDistrict = command.PermanentDistrict,
            //PermanentDivision = command.PermanentDivision,
            //PresentVillageAreaRoad = command.PresentVillageAreaRoad,
            //PresentPostOffice = command.PresentPostOffice,
            //PresentThana = command.PresentThana,
            //PresentDistrict = command.PresentDistrict,
            //PresentDivision = command.PresentDivision,
            IsActive = true
        };
    }
    public static HrmEmployeeMaster CreateCompleteEmployeeCommandToMaster(CreateCompleteEmployeeCommand command)
    {
        return new HrmEmployeeMaster
        {
            EmployeeCode = command.EmployeeCode,
            EmployeeNameEnglish = command.EmployeeNameEnglish,
            EmployeeNameBangla = command.EmployeeNameBangla,
            //CompanyId = command.CompanyId,
            //DepartmentId = command.DepartmentId,
            //SectionId = command.SectionId,
            //GradeId = command.GradeId,
            //DesignationId = command.DesignationId,
            //EmployeeType = command.EmployeeType,
            //ShiftId = command.ShiftId,
            //EmployeeNatureId = command.EmployeeNatureId,
            //Holiday = command.Holiday,
            //ProposedMonthlySalary = command.ProposedMonthlySalary,
            //JoiningDate = command.JoiningDate,
            //ConfirmationDate = command.ConfirmationDate,
            //Status = EmployeeStatus.Draft,
            IsActive = true
        };
    }
    public static EmployeeCompleteDto MapToEmployeeCompleteDto(this HrmEmployeeMaster employee)
    {
        return new EmployeeCompleteDto
        {
            // From EmployeeMaster
            Id = employee.Id,
            EnrollmentId = employee.EnrollmentId ?? string.Empty,
            EmployeeCode = employee.EmployeeCode,
            EmployeeNameEnglish = employee.EmployeeNameEnglish,
            EmployeeNameBangla = employee.EmployeeNameBangla,
            //CompanyId = employee.CompanyId,
            //CompanyName = employee.Company?.CompanyName ?? string.Empty,
            //DepartmentId = employee.DepartmentId,
            //DepartmentName = employee.Department?.DepartmentName ?? string.Empty,
            //SectionId = employee.SectionId,
            //SectionName = employee.Section?.SectionName ?? string.Empty,
            //GradeId = employee.GradeId,
            //GradeName = employee.Grade?.GradeName ?? string.Empty,
            //DesignationId = employee.DesignationId,
            //DesignationName = employee.Designation?.DesignationName ?? string.Empty,
            //EmployeeType = employee.EmployeeType,
            //ShiftId = employee.ShiftId,
            //ShiftName = employee.Shift?.ShiftName ?? string.Empty,
            //EmployeeNatureId = employee.EmployeeNatureId,
            //EmployeeNatureName = employee.EmployeeNature?.NatureName ?? string.Empty,
            //Holiday = employee.Holiday,
            //ProposedMonthlySalary = employee.ProposedMonthlySalary,
            //JoiningDate = employee.JoiningDate,
            //ConfirmationDate = employee.ConfirmationDate,
            Status = employee.Status,
            CreatedOn = employee.CreatedOn,
            CreatedBy = employee.CreatedBy,
            UpdatedOn = employee.UpdatedOn,
            UpdatedBy = employee.UpdatedBy,
            IsActive = employee.IsActive,

            // From EmployeePersonal
            PersonalInfoId = employee.Personal?.Id,
            DateOfBirth = employee.Personal?.DateOfBirth,
            Gender = employee.Personal?.Gender != null && Enum.TryParse<Utility.Enum.Gender>(employee.Personal.Gender, out var gender)
    ? gender
    : (Utility.Enum.Gender?)null,
MaritalStatus = employee.Personal?.MaritalStatus != null && Enum.TryParse<Utility.Enum.MaritalStatus>(employee.Personal.MaritalStatus, out var maritalStatus)
    ? maritalStatus
    : (Utility.Enum.MaritalStatus?)null,
MobileNumber = employee.Contact?.MobileNo,
            EmailAddress = employee.Contact?.PersonalEmail,
            DocumentType = employee.Documents?.FirstOrDefault()?.DocumentTypeId != null && Enum.TryParse<Utility.Enum.DocumentType>(employee.Documents.FirstOrDefault()?.DocumentTypeId, out var documentType)
    ? documentType
    : (Utility.Enum.DocumentType?)null,
DocumentNumber = employee.Documents?.FirstOrDefault()?.DocumentNo,
            //IdType = employee.Personal?.IdType,
            //IdNumber = employee.Personal?.IDNumber,
            //BloodGroup = employee.Personal?.BloodGroup,
            //Religion = employee.Personal?.Religion,
            //Nationality = employee.Personal?.Nationality,
            //FatherNameEnglish = employee.Personal?.FatherNameEnglish,
            //FatherNameBangla = employee.Personal?.FatherNameBangla,
            //MotherNameEnglish = employee.Personal?.MotherNameEnglish,
            //MotherNameBangla = employee.Personal?.MotherNameBangla,
            //SpouseName = employee.Personal?.SpouseName,
            //SpouseMobile = employee.Personal?.SpouseMobile,
            //TinNumber = employee.Personal?.IDNumber,
            //EmployeeReference = employee.Personal?.EmployeeReference,
            //ReferencePersonId = employee.Personal?.ReferencePersonId,
            //PermanentVillageAreaRoad = employee.Personal?.PermanentVillageAreaRoad,
            //PermanentPostOffice = employee.Personal?.PermanentPostOffice,
            //PermanentThana = employee.Personal?.PermanentThana,
            //PermanentDistrict = employee.Personal?.PermanentDistrict,
            //PermanentDivision = employee.Personal?.PermanentDivision,
            //PresentVillageAreaRoad = employee.Personal?.PresentVillageAreaRoad,
            //PresentPostOffice = employee.Personal?.PresentPostOffice,
            //PresentThana = employee.Personal?.PresentThana,
            //PresentDistrict = employee.Personal?.PresentDistrict,
            //PresentDivision = employee.Personal?.PresentDivision,

            // From EmployeeVerification
            VerificationInfoId = employee.Verification?.Id,
            SecurityClearanceBy = employee.Verification?.SecurityClearanceBy,
            SecurityClearanceDate = employee.Verification?.SecurityClearanceDate,
            EnrolledBy = employee.Verification?.EnrolledBy,
            EnrolledDate = employee.Verification?.EnrolledDate,
            BiometricEnrolledBy = employee.Verification?.BiometricEnrolledBy,
            BiometricEnrolledDate = employee.Verification?.BiometricEnrolledDate
        };
    }

}
