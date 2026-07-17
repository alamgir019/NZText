using NZ.HRM.Application.Employees.Queries.GetCandidateEntryReport;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDetailForIT;
using NZ.HRM.Application.Employees.Queries.GetMedicalReport;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Application.Model.EmployeeReports.DTOs;
using NZ.HRM.Mapping.Employees;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Employees.Handlers;

public class EmployeeQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public EmployeeQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<EmployeeDetailForIT?> Handle(GetEmployeeDetailForITQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        var dto = employee.MapToEmployeeDetailForIT();

        return dto;
    }

    public async Task<Queries.GetItActivationSummary.ItActivationSummaryDto> Handle(Queries.GetItActivationSummary.GetItActivationSummaryQuery query, CancellationToken cancellationToken = default)
    {
        // Fetch active employees up to now and filter by status
        var itActivated = await _employeeMasterRepository.GetByStatusUpToDateAsync(query, cancellationToken: cancellationToken);

        var dto = new Queries.GetItActivationSummary.ItActivationSummaryDto
        {
            Total = itActivated.Count,
            Workers = itActivated.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Worker.ToString(), StringComparison.OrdinalIgnoreCase)),
            Staff = itActivated.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Staff.ToString(), StringComparison.OrdinalIgnoreCase)),
            Management = itActivated.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Management.ToString(), StringComparison.OrdinalIgnoreCase))
        };
        var groupedByCompany = itActivated.GroupBy(e => e.Employment?.Unit?.UnitName);
        foreach (var group in groupedByCompany)
        {
            var companySummary = new Queries.GetItActivationSummary.ItActivationSummaryDto
            {
                CompanyName = group.Key,
                Total = group.Count(),
                Workers = group.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Worker.ToString(), StringComparison.OrdinalIgnoreCase)),
                Staff = group.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Staff.ToString(), StringComparison.OrdinalIgnoreCase)),
                Management = group.Count(e => string.Equals(e.EmployeeNature, EmployeeNature.Management.ToString(), StringComparison.OrdinalIgnoreCase))
            };
            dto.CompanySummaries.Add(companySummary);
        }
        return dto;
    }

    public async Task<MedicalReportDto?> Handle(GetMedicalReportQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        var medical = employee.MedicalFitnessCheck;
        if (medical == null)
            return null;

        var personal = employee.Personal;
        var employment = employee.Employment;

        // Calculate age
        var age = string.Empty;
        if (personal?.DateOfBirth.HasValue == true)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var ageYears = today.Year - personal.DateOfBirth.Value.Year;
            if (personal.DateOfBirth.Value > today.AddYears(-ageYears))
                ageYears--;
            age = $"{ageYears} years";
        }

        // Find photo document
        var photoDoc = employee.Documents?.FirstOrDefault(d => 
            string.Equals(d.DocumentType, "Photo", StringComparison.OrdinalIgnoreCase) || 
            string.Equals(d.DocumentType, "PassportPhoto", StringComparison.OrdinalIgnoreCase));

        return new MedicalReportDto
        {
            SlipNo = medical.Id,
            Date = medical.ExaminationDateTime.ToString("yyyy-MM-dd"),
            EnrollmentId = employee.EnrollmentId ?? string.Empty,
            EmployeeCode = employee.EmployeeCode ?? string.Empty,
            Name = employee.EmployeeName ?? employee.EmployeeNameBangla ?? string.Empty,
            FatherName = personal?.FatherName ?? personal?.FatherNameBangla ?? string.Empty,
            MotherName = personal?.MotherName ?? personal?.MotherNameBangla ?? string.Empty,
            DateOfBirth = personal?.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
            Age = age,
            Gender = personal?.Gender ?? string.Empty,
            BloodGroup = personal?.BloodGroup ?? string.Empty,
            Village = personal?.PresentVillageAreaRoad ?? string.Empty,
            PostOffice = personal?.PresentPostOffice ?? string.Empty,
            PoliceStation = personal?.PresentThana?.ThanaName ?? string.Empty,
            Upazila = personal?.PresentThana?.ThanaName ?? string.Empty,
            District = personal?.PresentDistrict?.DistrictName ?? string.Empty,
            Company = employment?.Unit?.UnitName ?? string.Empty,
            Unit = employment?.Subunit?.SubunitName ?? string.Empty,
            Department = employment?.Department?.DepartmentName ?? string.Empty,
            Designation = employment?.Designation?.DesignationName ?? string.Empty,
            Height = string.Empty, // Not available in current schema
            Weight = string.Empty, // Not available in current schema
            TestedBloodGroup = string.Empty, // Not available in current schema
            IdentificationMark = medical.IdentificationSign ?? string.Empty,
            DoctorName = medical.ExaminedByDoctor ?? string.Empty,
            DoctorQualification = string.Empty, // Not available in current schema
            IsFit = string.Equals(medical.Fitness, "Fit", StringComparison.OrdinalIgnoreCase),
            Remarks = medical.Remarks ?? string.Empty,
            PhotoUrl = photoDoc?.FilePath
        };
    }

    public async Task<CandidateEntryReportDto?> Handle(GetCandidateEntryReportQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        var personal = employee.Personal;
        var employment = employee.Employment;
        var verification = employee.Verification;
        var nominee = employee.Nominees?.FirstOrDefault();

        // Calculate age
        int? age = null;
        if (personal?.DateOfBirth.HasValue == true)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var ageYears = today.Year - personal.DateOfBirth.Value.Year;
            if (personal.DateOfBirth.Value > today.AddYears(-ageYears))
                ageYears--;
            age = ageYears;
        }

        return new CandidateEntryReportDto
        {
            // Employee Master
            EmployeeId = employee.Id,
            EnrollmentId = employee.EnrollmentId ?? string.Empty,
            EmployeeNameBangla = employee.EmployeeNameBangla ?? string.Empty,
            Status = employee.Status ?? string.Empty,

            // Employment Information
            UnitId = employment?.UnitId ?? string.Empty,
            UnitName = employment?.Unit?.UnitName ?? string.Empty,
            DesignationId = employment?.DesignationId,
            DesignationName = employment?.Designation?.DesignationName,
            ProposedMonthlySalary = employee.Payroll?.ProposedSalary,
            JoiningDate = employment?.JoiningDate?.ToString("yyyy-MM-dd") ?? string.Empty,

            // Personal Information
            DateOfBirth = personal?.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
            Age = age,
            Gender = personal?.Gender ?? string.Empty,
            Religion = personal?.Religion ?? string.Empty,
            BloodGroup = personal?.BloodGroup,
            IDType = personal?.IdType,
            IDNumber = personal?.IdNumber,
            MobileNumber = personal?.MobileNumber ?? string.Empty,

            // Family Information
            GuardianType = personal?.GuardianType?.ToString(),
            GuardianNameBangla = personal?.GuardianNameBangla,
            FatherNameBangla = personal?.FatherNameBangla ?? string.Empty,
            MotherNameBangla = personal?.MotherNameBangla,
            EmployeeReference = personal?.EmployeeReference,
            ReferenceMobileNumber = personal?.ReferenceMobileNumber,

            // Permanent Address
            PermanentVillageAreaRoad = personal?.PermanentVillageAreaRoad,
            PermanentPostOffice = personal?.PermanentPostOffice,
            PermanentThanaId = personal?.PermanentThanaId,
            PermanentThanaName = personal?.PermanentThana?.ThanaName,
            PermanentDistrictId = personal?.PermanentDistrictId,
            PermanentDistrictName = personal?.PermanentDistrict?.DistrictName,
            PermanentDivisionId = personal?.PermanentDivisionId,
            PermanentDivisionName = personal?.PermanentDivision?.DivisionName,

            // Present Address
            PresentVillageAreaRoad = personal?.PresentVillageAreaRoad,
            PresentPostOffice = personal?.PresentPostOffice,
            PresentThanaId = personal?.PresentThanaId,
            PresentThanaName = personal?.PresentThana?.ThanaName,
            PresentDistrictId = personal?.PresentDistrictId,
            PresentDistrictName = personal?.PresentDistrict?.DistrictName,
            PresentDivisionId = personal?.PresentDivisionId,
            PresentDivisionName = personal?.PresentDivision?.DivisionName,

            // Nominee Information
            NomineeNameBangla = nominee?.NomineeNameBangla,
            NomineeRelationBangla = nominee?.RelationshipBangla,

            // Verification Information
            SecurityClearanceBy = verification?.SecurityClearanceBy,
            SecurityClearanceDate = verification?.SecurityClearanceDate?.ToString("yyyy-MM-dd"),
            EnrolledBy = verification?.EnrolledBy,
            EnrolledDate = verification?.EnrolledDate?.ToString("yyyy-MM-dd"),
            BiometricEnrolledBy = verification?.BiometricEnrolledBy,
            BiometricEnrolledDate = verification?.BiometricEnrolledDate?.ToString("yyyy-MM-dd"),

            // Metadata
            CreatedOn = employee.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = employee.CreatedBy,
            ModifiedOn = employee.UpdatedOn.ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = employee.UpdatedBy
        };
    }
}

