using NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Mapping.Employees;

public static class EmployeeMapper
{
    public static EmployeePersonal CreateCompleteEmployeeCommandToPersonal(CreateCompleteEmployeeCommand command, string employeeId)
    {
        return new EmployeePersonal
        {
            EmployeeId = employeeId,
            DateOfBirth = command.DateOfBirth,
            Gender = command.Gender,
            MaritalStatus = command.MaritalStatus,
            MobileNumber = command.MobileNumber,
            EmailAddress = command.EmailAddress,
            DocumentType = command.DocumentType,
            DocumentNumber = command.DocumentNumber,
            BloodGroup = command.BloodGroup,
            Religion = command.Religion,
            Nationality = command.Nationality,
            FatherNameEnglish = command.FatherNameEnglish,
            FatherNameBangla = command.FatherNameBangla,
            MotherNameEnglish = command.MotherNameEnglish,
            MotherNameBangla = command.MotherNameBangla,
            SpouseName = command.SpouseName,
            SpouseMobile = command.SpouseMobile,
            IDNumber = command.TinNumber,
            EmployeeReference = command.EmployeeReference,
            ReferencePersonId = command.ReferencePersonId,
            PermanentVillageAreaRoad = command.PermanentVillageAreaRoad,
            PermanentPostOffice = command.PermanentPostOffice,
            PermanentThana = command.PermanentThana,
            PermanentDistrict = command.PermanentDistrict,
            PermanentDivision = command.PermanentDivision,
            PresentVillageAreaRoad = command.PresentVillageAreaRoad,
            PresentPostOffice = command.PresentPostOffice,
            PresentThana = command.PresentThana,
            PresentDistrict = command.PresentDistrict,
            PresentDivision = command.PresentDivision,
            IsActive = true
        };
    }
    public static EmployeeMaster CreateCompleteEmployeeCommandToMaster(CreateCompleteEmployeeCommand command)
    {
        return new EmployeeMaster
        {
            EmployeeCode = command.EmployeeCode,
            EmployeeNameEnglish = command.EmployeeNameEnglish,
            EmployeeNameBangla = command.EmployeeNameBangla,
            CompanyId = command.CompanyId,
            DepartmentId = command.DepartmentId,
            SectionId = command.SectionId,
            GradeId = command.GradeId,
            DesignationId = command.DesignationId,
            EmployeeType = command.EmployeeType,
            Shift = command.Shift,
            EmployeeNature = command.EmployeeNature,
            Holiday = command.Holiday,
            ProposedMonthlySalary = command.ProposedMonthlySalary,
            JoiningDate = command.JoiningDate,
            ConfirmationDate = command.ConfirmationDate,
            Status = EmployeeStatus.Draft,
            IsActive = true
        };
    }

    public static EmployeeCompleteDto MapToEmployeeCompleteDto(this EmployeeMaster employee)
    {
        return new EmployeeCompleteDto
        {
            // From EmployeeMaster
            Id = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            EmployeeNameEnglish = employee.EmployeeNameEnglish,
            EmployeeNameBangla = employee.EmployeeNameBangla,
            CompanyId = employee.CompanyId,
            CompanyName = employee.Company?.CompanyName ?? string.Empty,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.DepartmentName ?? string.Empty,
            SectionId = employee.SectionId,
            SectionName = employee.Section?.SectionName ?? string.Empty,
            GradeId = employee.GradeId,
            GradeName = employee.Grade?.GradeName ?? string.Empty,
            DesignationId = employee.DesignationId,
            DesignationName = employee.Designation?.DesignationName ?? string.Empty,
            EmployeeType = employee.EmployeeType,
            Shift = employee.Shift,
            EmployeeNature = employee.EmployeeNature,
            Holiday = employee.Holiday,
            ProposedMonthlySalary = employee.ProposedMonthlySalary,
            JoiningDate = employee.JoiningDate,
            ConfirmationDate = employee.ConfirmationDate,
            Status = employee.Status,
            CreatedOn = employee.CreatedOn,
            CreatedBy = employee.CreatedBy,
            UpdatedOn = employee.UpdatedOn,
            UpdatedBy = employee.UpdatedBy,
            IsActive = employee.IsActive,

            // From EmployeePersonal
            PersonalInfoId = employee.PersonalInfo?.Id,
            DateOfBirth = employee.PersonalInfo?.DateOfBirth,
            Gender = employee.PersonalInfo?.Gender,
            MaritalStatus = employee.PersonalInfo?.MaritalStatus,
            MobileNumber = employee.PersonalInfo?.MobileNumber,
            EmailAddress = employee.PersonalInfo?.EmailAddress,
            DocumentType = employee.PersonalInfo?.DocumentType,
            DocumentNumber = employee.PersonalInfo?.DocumentNumber,
            IdType = employee.PersonalInfo?.IdType,
            IdNumber = employee.PersonalInfo?.IDNumber,
            BloodGroup = employee.PersonalInfo?.BloodGroup,
            Religion = employee.PersonalInfo?.Religion,
            Nationality = employee.PersonalInfo?.Nationality,
            FatherNameEnglish = employee.PersonalInfo?.FatherNameEnglish,
            FatherNameBangla = employee.PersonalInfo?.FatherNameBangla,
            MotherNameEnglish = employee.PersonalInfo?.MotherNameEnglish,
            MotherNameBangla = employee.PersonalInfo?.MotherNameBangla,
            SpouseName = employee.PersonalInfo?.SpouseName,
            SpouseMobile = employee.PersonalInfo?.SpouseMobile,
            TinNumber = employee.PersonalInfo?.IDNumber,
            EmployeeReference = employee.PersonalInfo?.EmployeeReference,
            ReferencePersonId = employee.PersonalInfo?.ReferencePersonId,
            PermanentVillageAreaRoad = employee.PersonalInfo?.PermanentVillageAreaRoad,
            PermanentPostOffice = employee.PersonalInfo?.PermanentPostOffice,
            PermanentThana = employee.PersonalInfo?.PermanentThana,
            PermanentDistrict = employee.PersonalInfo?.PermanentDistrict,
            PermanentDivision = employee.PersonalInfo?.PermanentDivision,
            PresentVillageAreaRoad = employee.PersonalInfo?.PresentVillageAreaRoad,
            PresentPostOffice = employee.PersonalInfo?.PresentPostOffice,
            PresentThana = employee.PersonalInfo?.PresentThana,
            PresentDistrict = employee.PersonalInfo?.PresentDistrict,
            PresentDivision = employee.PersonalInfo?.PresentDivision,

            // From EmployeeVerification
            VerificationInfoId = employee.VerificationInfo?.Id,
            SecurityClearanceBy = employee.VerificationInfo?.SecurityClearanceBy,
            SecurityClearanceDate = employee.VerificationInfo?.SecurityClearanceDate,
            EnrolledBy = employee.VerificationInfo?.EnrolledBy,
            EnrolledDate = employee.VerificationInfo?.EnrolledDate,
            BiometricEnrolledBy = employee.VerificationInfo?.BiometricEnrolledBy,
            BiometricEnrolledDate = employee.VerificationInfo?.BiometricEnrolledDate
        };
    }

}
