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

    public static EmployeeDetailDto MapToEmployeeDetailDto(this HrmEmployeeMaster employee)
    {
        return new EmployeeDetailDto
        {
            Id = employee.Id,
            EnrollmentId = employee.EnrollmentId ?? string.Empty,
            EmployeeCode = employee.EmployeeCode,
            EmployeeNameEnglish = employee.EmployeeName,
            EmployeeNameBangla = employee.EmployeeNameBangla,
            Gender = employee.Personal?.Gender != null && Enum.TryParse<Utility.Enum.Gender>(employee.Personal.Gender, out var gender)
            ? gender : null,
            BloodGroup = employee.Personal?.BloodGroup != null && Enum.TryParse<Utility.Enum.BloodGroup>(employee.Personal.BloodGroup, out var bloodGroup)
            ? bloodGroup : null,
            JoiningDate = employee.Employment?.JoiningDate,
            EmployeeType = employee.EmployeeType != null && Enum.TryParse<EmployeeType>(employee.EmployeeType, out var employeeType)
            ? employeeType : null,
            EmployeeName = employee.EmployeeName,
            UnitName = employee.Employment?.Unit?.UnitName ?? string.Empty,
            SubUnitName = employee.Employment?.Subunit?.SubunitName ?? string.Empty,
            CellName = employee.Employment?.Cell?.CellName ?? string.Empty,
            DepartmentName = employee.Employment?.Department?.DepartmentName ?? string.Empty,
            SectionName = employee.Employment?.Section?.SectionName ?? string.Empty,
            GradeName = employee.Employment?.Grade?.GradeName ?? string.Empty,
            DesignationName = employee.Employment?.Designation?.DesignationName ?? string.Empty,
            ShiftName = employee.Employment?.Shift?.ShiftName ?? string.Empty,
            ProposedMonthlySalary = employee.Payroll?.ProposedSalary
        };
    }

    public static EmployeeDetailForIT MapToEmployeeDetailForIT(this HrmEmployeeMaster employee)
    {
        // Map shared/base properties first using the existing mapper
        var baseDto = employee.MapToEmployeeDetailDto();

        // Create EmployeeDetailForIT and copy base properties
        var employeeDetailDto = new EmployeeDetailForIT
        {
            Id = baseDto.Id,
            EnrollmentId = baseDto.EnrollmentId,
            EmployeeCode = baseDto.EmployeeCode,
            EmployeeNameEnglish = baseDto.EmployeeNameEnglish,
            EmployeeNameBangla = baseDto.EmployeeNameBangla,
            EmployeeName = baseDto.EmployeeName,
            UnitName = baseDto.UnitName,
            UnitId = employee.Employment?.UnitId,
            SubUnitName = baseDto.SubUnitName,
            SubUnitId = employee.Employment?.SubunitId,
            DepartmentName = baseDto.DepartmentName,
            DepartmentId = employee.Employment?.DepartmentId,
            SectionName = baseDto.SectionName,
            SectionId = employee.Employment?.SectionId,
            CellName = baseDto.CellName,
            CellId = employee.Employment?.CellId,
            GradeName = baseDto.GradeName,
            GradeId = employee.Employment?.GradeId,
            DesignationName = baseDto.DesignationName,
            DesignationId = employee.Employment?.DesignationId,
            ShiftName = baseDto.ShiftName,
            ShiftId = employee.Employment?.ShiftId,
            ProposedMonthlySalary = baseDto.ProposedMonthlySalary,
            Gender = baseDto.Gender,
            BloodGroup = baseDto.BloodGroup,
            JoiningDate = baseDto.JoiningDate,
            EmployeeType = baseDto.EmployeeType,
            FatherName = employee.Personal?.FatherName,
            FatherNameBangla = employee.Personal?.FatherNameBangla,
            MotherName = employee.Personal?.MotherName,
            MotherNameBangla = employee.Personal?.MotherNameBangla,
            DateOfBirth = employee.Personal?.DateOfBirth,
            Religion = employee.Personal?.Religion,
            NomineeName = employee.Nominees.FirstOrDefault()?.NomineeName,
            NomineeRelation = employee.Nominees.FirstOrDefault()?.Relationship,
            Mobile = employee.Contact?.MobileNo,
            ApprovedByDirector = null,
            Department = employee.Employment?.Department?.DepartmentName,
            WeekOffDay = employee.Employment?.WeeklyOffDay,
            ProbationPeriod = null,
            ReportingTo = employee.Reportings.FirstOrDefault()?.ReportingEmployee?.EmployeeName,
            Documents = employee.Documents?.Select(d => new EmployeeDocumentDto
            {
                EmployeeId = d.EmployeeId,
                DocumentNo = d.DocumentNo,
                DocumentType = string.IsNullOrWhiteSpace(d.DocumentType) ? null : Enum.TryParse<Utility.Enum.DocumentType>(d.DocumentType, out var dt) ? dt : null,
                IssueDate = d.IssueDate,
                ExpiryDate = d.ExpiryDate,
                FileName = d.FileName,
                FilePath = d.FilePath
            }).ToArray()
        };

        return employeeDetailDto;
    }
}
