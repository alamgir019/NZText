using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Model.EmployeeReports.DTOs;

public class CandidateEntryReportDto
{
    // Employee Master Information
    public string EmployeeId { get; set; } = string.Empty;
    public string EnrollmentId { get; set; } = string.Empty;
    public string EmployeeNameBangla { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    // Employment Information
    public string UnitId { get; set; } = string.Empty;
    public string UnitName { get; set; } = string.Empty;
    public string? DesignationId { get; set; }
    public string? DesignationName { get; set; }
    public decimal? ProposedMonthlySalary { get; set; }
    public string JoiningDate { get; set; } = string.Empty;

    // Personal Information
    public string DateOfBirth { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Religion { get; set; } = string.Empty;
    public string? BloodGroup { get; set; }
    public IDType? IDType { get; set; }
    public string? IDNumber { get; set; }
    public string MobileNumber { get; set; } = string.Empty;

    // Family Information
    public string? GuardianType { get; set; }
    public string? GuardianNameBangla { get; set; }
    public string FatherNameBangla { get; set; } = string.Empty;
    public string? MotherNameBangla { get; set; }
    public string? EmployeeReference { get; set; }
    public string? ReferenceMobileNumber { get; set; }

    // Permanent Address
    public string? PermanentVillageAreaRoad { get; set; }
    public string? PermanentPostOffice { get; set; }
    public string? PermanentThanaId { get; set; }
    public string? PermanentThanaName { get; set; }
    public string? PermanentDistrictId { get; set; }
    public string? PermanentDistrictName { get; set; }
    public string? PermanentDivisionId { get; set; }
    public string? PermanentDivisionName { get; set; }

    // Present Address
    public string? PresentVillageAreaRoad { get; set; }
    public string? PresentPostOffice { get; set; }
    public string? PresentThanaId { get; set; }
    public string? PresentThanaName { get; set; }
    public string? PresentDistrictId { get; set; }
    public string? PresentDistrictName { get; set; }
    public string? PresentDivisionId { get; set; }
    public string? PresentDivisionName { get; set; }

    // Nominee Information
    public string? NomineeNameBangla { get; set; }
    public string? NomineeRelationBangla { get; set; }

    // Verification Information
    public string? SecurityClearanceBy { get; set; }
    public string? SecurityClearanceDate { get; set; }
    public string? EnrolledBy { get; set; }
    public string? EnrolledDate { get; set; }
    public string? BiometricEnrolledBy { get; set; }
    public string? BiometricEnrolledDate { get; set; }

    // Metadata
    public string CreatedOn { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
    public string? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}
