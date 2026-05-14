using NZ.HRM.Application.Employees.DTOs;
using NZ.HRM.Application.Employees.Queries.GetCompleteEmployee;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Employees.Handlers;

public class CompleteEmployeeQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public CompleteEmployeeQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<EmployeeCompleteDto?> Handle(GetCompleteEmployeeQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);

        if (employee == null)
            return null;

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
