using NZ.HRM.Application.Employees.Queries.GetCandidateEntryReport;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDetailForIT;
using NZ.HRM.Application.Employees.Queries.GetEmployeeMasterList;
using NZ.HRM.Application.Employees.Queries.GetMedicalReport;
using NZ.HRM.Application.Employees.Queries.GetEmployeeDetailedProfile;
using NZ.HRM.Application.Employees.Queries.GetJoiningLetter;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Application.Model.EmployeeReports.DTOs;
using NZ.HRM.Mapping.Employees;
using NZ.HRM.Utility;
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
            Name = employee.EmployeeNameBangla ?? employee.EmployeeName ?? string.Empty,
            FatherName = personal?.FatherNameBangla ?? personal?.FatherName ?? string.Empty,
            MotherName = personal?.MotherNameBangla ?? personal?.MotherName ?? string.Empty,
            DateOfBirth = personal?.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
            Age = age,
            Gender = EnumHelper.TryParseEnum<Gender>(personal?.Gender),
            BloodGroup = EnumHelper.TryParseEnum<BloodGroup>(personal?.BloodGroup),
            Village = personal?.PresentVillageAreaRoadBangla ?? string.Empty,
            PostOffice = personal?.PresentPostOfficeBangla ?? string.Empty,
            Upazila = personal?.PresentThana?.ThanaNameBangla ?? string.Empty,
            District = personal?.PresentDistrict?.DistrictNameBangla ?? string.Empty,
            Division = personal?.PresentDivision?.DivisionNameBangla ?? string.Empty,
            Company = employment?.Unit?.UnitNameBangla ?? string.Empty,
            Subunit = employment?.Subunit?.SubunitNameBangla ?? string.Empty,
            Department = employment?.Department?.DepartmentNameBangla ?? string.Empty,
            Designation = employment?.Designation?.DesignationNameBangla ?? string.Empty,
            Height = string.Empty, // Not available in current schema
            Weight = string.Empty, // Not available in current schema
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

        return new CandidateEntryReportDto
        {
            // Employee Master
            EmployeeId = employee.Id,
            EnrollmentId = employee.EnrollmentId ?? string.Empty,
            EmployeeNameBangla = employee.EmployeeNameBangla ?? string.Empty,
            Status = employee.Status ?? string.Empty,

            // Employment Information
            UnitId = employment?.UnitId ?? string.Empty,
            UnitName = employment?.Unit?.UnitNameBangla ?? string.Empty,
            DesignationId = employment?.DesignationId,
            DesignationName = employment?.Designation?.DesignationNameBangla,
            ProposedMonthlySalary = employee.Payroll?.ProposedSalary,
            JoiningDate = employment?.JoiningDate?.ToString("yyyy-MM-dd") ?? string.Empty,

            // Personal Information
            DateOfBirth = personal?.DateOfBirth?.ToString("yyyy-MM-dd") ?? string.Empty,
            Gender = EnumHelper.TryParseEnum<Gender>(personal?.Gender),
            Religion = EnumHelper.TryParseEnum<Religion>(personal?.Religion),
            BloodGroup = EnumHelper.TryParseEnum<BloodGroup>(personal?.BloodGroup),
            IDType = personal?.IdType,
            IDNumber = personal?.IdNumber,
            MobileNumber = personal?.MobileNumber ?? string.Empty,

            // Family Information
            GuardianType = personal?.GuardianType,
            GuardianNameBangla = personal?.GuardianNameBangla,
            FatherNameBangla = personal?.FatherNameBangla ?? string.Empty,
            MotherNameBangla = personal?.MotherNameBangla,
            EmployeeReferenceBangla = personal?.EmployeeReferenceBangla,
            EmployeeReference = personal?.EmployeeReference,
            ReferenceMobileNumber = personal?.ReferenceMobileNumber,

            // Permanent Address
            PermanentVillageAreaRoad = personal?.PermanentVillageAreaRoad,
            PermanentPostOffice = personal?.PermanentPostOffice,
            PermanentVillageAreaRoadBangla = personal?.PermanentVillageAreaRoadBangla,
            PermanentPostOfficeBangla = personal?.PermanentPostOfficeBangla,
            PermanentThanaId = personal?.PermanentThanaId,
            PermanentThanaName = personal?.PermanentThana?.ThanaNameBangla,
            PermanentDistrictId = personal?.PermanentDistrictId,
            PermanentDistrictName = personal?.PermanentDistrict?.DistrictNameBangla,
            PermanentDivisionId = personal?.PermanentDivisionId,
            PermanentDivisionName = personal?.PermanentDivision?.DivisionNameBangla,

            // Present Address
            PresentVillageAreaRoad = personal?.PresentVillageAreaRoad,
            PresentPostOffice = personal?.PresentPostOffice,
            PresentVillageAreaRoadBangla = personal?.PresentVillageAreaRoadBangla,
            PresentPostOfficeBangla = personal?.PresentPostOfficeBangla,
            PresentThanaId = personal?.PresentThanaId,
            PresentThanaName = personal?.PresentThana?.ThanaNameBangla,
            PresentDistrictId = personal?.PresentDistrictId,
            PresentDistrictName = personal?.PresentDistrict?.DistrictNameBangla,
            PresentDivisionId = personal?.PresentDivisionId,
            PresentDivisionName = personal?.PresentDivision?.DivisionNameBangla,

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

    public async Task<EmployeeMasterListResponseDto> Handle(GetEmployeeMasterListQuery query, CancellationToken cancellationToken = default)
    {
        // Create filter request object from query
        var filterRequest = new EmployeeMasterListFilterRequest
        {
            UnitId = query.UnitId,
            SubUnitId = query.SubUnitId,
            DepartmentId = query.DepartmentId,
            SectionId = query.SectionId,
            CellId = query.CellId,
            EmployeeNature = query.EmployeeNature,
            JoiningFromDate = query.JoiningFromDate,
            JoiningToDate = query.JoiningToDate,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            IncludeInactive = query.IncludeInactive
        };

        // Call repository method with filter request object
        var (employees, totalCount) = await _employeeMasterRepository.GetEmployeeMasterListAsync(filterRequest, cancellationToken);

        // Map to DTOs
        var employeeDtos = employees.Select(e => new EmployeeMasterListItemDto
        {
            EmployeeId = e.Id,
            EmployeeCode = e.EmployeeCode ?? string.Empty,
            EmployeeName = e.EmployeeName ?? e.EmployeeNameBangla ?? string.Empty,
            DepartmentName = e.Employment?.Department?.DepartmentName ?? string.Empty,
            SectionName = e.Employment?.Section?.SectionName ?? string.Empty,
            CellName = e.Employment?.Cell?.CellName ?? string.Empty,
            DesignationName = e.Employment?.Designation?.DesignationName ?? string.Empty,
            EmployeeNature = e.EmployeeNature ?? string.Empty,
            JoiningDate = e.Employment?.JoiningDate?.ToString("yyyy-MM-dd"),
            IsActive = e.IsActive
        }).ToList();

        var validPageNumber = Math.Max(1, query.PageNumber);
        var validPageSize = Math.Max(1, Math.Min(query.PageSize, 1000));

        return new EmployeeMasterListResponseDto
        {
            Employees = employeeDtos,
            TotalCount = totalCount,
            PageNumber = validPageNumber,
            PageSize = validPageSize
        };
    }

    public async Task<EmployeeDetailedProfileDto?> Handle(GetEmployeeDetailedProfileQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByEmployeeCodeAsync(query.EmployeeCode, cancellationToken);
        if (employee == null)
            return null;

        var personal = employee.Personal;
        var employment = employee.Employment;
        var nominee = employee.Nominees?.FirstOrDefault();
        var medical = employee.MedicalFitnessCheck;

        // Build comprehensive profile DTO
        var profileDto = new EmployeeDetailedProfileDto
        {
            // Left Panel Information
            EmployeeId = employee.Id,
            EnrollmentId = employee.EnrollmentId ?? string.Empty,
            EmployeeCode = employee.EmployeeCode ?? string.Empty,
            DateOfJoining = employment?.JoiningDate?.ToString("dd-MMM-yyyy"),
            EmploymentType = employee.EmployeeNature,
            Status = employee.IsActive ? "Active" : "Inactive",
            IsActive = employee.IsActive,

            // Personal Information
            FullName = employee.EmployeeName ?? employee.EmployeeNameBangla ?? string.Empty,
            FatherName = personal?.FatherName ?? personal?.FatherNameBangla,
            DateOfBirth = personal?.DateOfBirth?.ToString("dd-MMM-yyyy"),
            Gender = EnumHelper.TryParseEnum<Gender>(personal?.Gender),
            BloodGroup = EnumHelper.TryParseEnum<BloodGroup>(personal?.BloodGroup),
            Religion = EnumHelper.TryParseEnum<Religion>(personal?.Religion),
            Nationality = personal?.Nationality,
            IDNumber = personal?.IdNumber,
            Mobile = personal?.MobileNumber,

            // Service Information
            Company = employment?.Unit?.UnitName ?? employment?.Unit?.UnitNameBangla,
            Department = employment?.Department?.DepartmentName ?? employment?.Department?.DepartmentNameBangla,
            Section = employment?.Section?.SectionName,
            Cell = employment?.Cell?.CellName,
            Designation = employment?.Designation?.DesignationName,
            Grade = employment?.Grade?.GradeName,
            Shift = employment?.Shift?.ShiftName,
            WeeklyOff = employment?.WeeklyOffDay,
            ReportingTo = employment?.ReportingTo,

            // Salary Information
            BasicSalary = employee.Payroll?.BasicSalary,
            HouseRent = employee.Payroll?.HouseRentAllowance,
            ConveyanceAllowance = employee.Payroll?.ConveyanceAllowance,
            MedicalAllowance = employee.Payroll?.MedicalAllowance,
            FoodAllowance = employee.Payroll?.FoodAllowance,
            OtherAllowances = employee.Payroll?.OtherAllowance, // Would need separate salary structure data
            GrossSalary = employee.Payroll?.GrossSalary,
            MonthlySalary = employee.Payroll?.ProposedSalary,

            // Address Information
            PresentAddress = new AddressInformationDto
            {
                VillageAreaRoad = personal?.PresentVillageAreaRoad,
                PostOffice = personal?.PresentPostOffice,
                ThanaName = personal?.PresentThana?.ThanaName,
                DistrictName = personal?.PresentDistrict?.DistrictName,
                DivisionName = personal?.PresentDivision?.DivisionName,
            },
            PermanentAddress = new AddressInformationDto
            {
                VillageAreaRoad = personal?.PermanentVillageAreaRoad,
                PostOffice = personal?.PermanentPostOffice,
                ThanaName = personal?.PermanentThana?.ThanaName,
                DistrictName = personal?.PermanentDistrict?.DistrictName,
                DivisionName = personal?.PermanentDivision?.DivisionName,
            },

            // Nominee Information
            NomineeInfo = nominee != null ? new NomineeInformationDto
            {
                NomineeName = nominee.NomineeName,
                Relation = nominee.Relationship,
                Mobile = nominee.MobileNo,
                Address = nominee.Address
            } : null,

            // Medical Information
            MedicalInfo = medical != null ? new MedicalInformationDto
            {
                MedicalStatus = medical.Fitness,
                DateOfMedical = medical.ExaminationDateTime.ToString("dd-MMM-yyyy"),
                MedicalCenter = string.Empty,
                BloodGroupMedical = personal?.BloodGroup
            } : null,

            // Documents Summary
            Documents = employee.Documents?.Select(d => new DocumentSummaryDto
            {
                DocumentType = EnumHelper.TryParseEnum<DocumentType>(d.DocumentType),
                Status = d.IsActive ? "Verified" : "Pending",
                FilePath = d.FilePath,
                IsAvailable = !string.IsNullOrEmpty(d.FilePath)
            }).ToList() ?? new List<DocumentSummaryDto>(),

        };

        return profileDto;
    }

    public async Task<JoiningLetterDto?> Handle(GetJoiningLetterQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        var personal = employee.Personal;
        var employment = employee.Employment;
        var payroll = employee.Payroll;

        // Build present address
        var presentAddress = string.Join(", ",
            new[] {
                personal?.PresentVillageAreaRoadBangla,
                personal?.PresentPostOfficeBangla,
                personal?.PresentThana?.ThanaNameBangla,
                personal?.PresentDistrict?.DistrictNameBangla,
                personal?.PresentDivision?.DivisionNameBangla
            }.Where(x => !string.IsNullOrWhiteSpace(x))
        );

        // Build permanent address
        var permanentAddress = string.Join(", ",
            new[] {
                personal?.PermanentVillageAreaRoadBangla,
                personal?.PermanentPostOfficeBangla,
                personal?.PermanentThana?.ThanaNameBangla,
                personal?.PermanentDistrict?.DistrictNameBangla,
                personal?.PermanentDivision?.DivisionNameBangla
            }.Where(x => !string.IsNullOrWhiteSpace(x))
        );

        return new JoiningLetterDto
        {
            CurrentDate = DateTime.Now.ToString("dd-MMM-yyyy"),
            EmployeeId = employee.Id,
            EmployeeCode = employee.EmployeeCode,
            EmployeeNameBangla = employee.EmployeeNameBangla ?? string.Empty,
            FatherNameBangla = personal?.FatherNameBangla ?? string.Empty,
            MotherNameBangla = personal?.MotherNameBangla ?? string.Empty,
            SpouseNameBangla = personal?.SpouseNameBangla ?? personal?.SpouseName,
            PresentAddressBangla = presentAddress,
            PermanentAddressBangla = permanentAddress,
            JoiningDate = employment?.JoiningDate?.ToString("dd-MMM-yyyy") ?? string.Empty,
            GradeBangla = employment?.Grade?.GradeNameBangla ?? employment?.Grade?.GradeName,
            DesignationBangla = employment?.Designation?.DesignationNameBangla ?? employment?.Designation?.DesignationName,
            DepartmentBangla = employment?.Department?.DepartmentNameBangla ?? employment?.Department?.DepartmentName,
            SectionBangla = employment?.Section?.SectionNameBangla ?? employment?.Section?.SectionName,
            BasicSalary = payroll?.BasicSalary?.ToString("0.00") ?? "0.00",
            HouseRent = payroll?.HouseRentAllowance?.ToString("0.00") ?? "0.00",
            MedicalAllowance = payroll?.MedicalAllowance?.ToString("0.00") ?? "0.00",
            ConveyanceAllowance = payroll?.ConveyanceAllowance?.ToString("0.00") ?? "0.00",
            FoodAllowance = payroll?.FoodAllowance?.ToString("0.00") ?? "0.00",
            GrossSalary = payroll?.GrossSalary?.ToString("0.00") ?? "0.00"
        };
    }
}

