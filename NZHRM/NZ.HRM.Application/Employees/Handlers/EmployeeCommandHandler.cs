using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Mapping.Employees;
using NZ.HRM.Utility.Enum;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace NZ.HRM.Application.Employees.Handlers;

public class EmployeeCommandHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;
    private readonly IEmployeePersonalRepository _employeePersonalRepository;
    private readonly IPayrollRepository _employeePayrollRepository;
    private readonly IEmployeeVerificationRepository _employeeVerificationRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IEmployeeNatureRepository _employeeNatureRepository;
    private readonly IEmployeeEmploymentRepository _employeeEmploymentRepository;
    private readonly IEmployeeDocumentRepository _employeeDocumentRepository;
    private readonly IEmployeeSalaryAccountRepository _employeeSalaryAccountRepository;
    private readonly IEmployeeNomineeRepository _employeeNomineeRepository;

    public EmployeeCommandHandler(
        IEmployeeMasterRepository employeeMasterRepository,
        IEmployeePersonalRepository employeePersonalRepository,
        IPayrollRepository employeePayrollRepository,
        IEmployeeVerificationRepository employeeVerificationRepository,
        IUnitRepository unitRepository,
        IDepartmentRepository departmentRepository,
        ISectionRepository sectionRepository,
        IShiftRepository shiftRepository,
        IEmployeeNatureRepository employeeNatureRepository,
        IEmployeeEmploymentRepository employeeEmploymentRepository,
        IEmployeeSalaryAccountRepository employeeSalaryAccountRepository,
        IEmployeeDocumentRepository employeeDocumentRepository,
        IEmployeeNomineeRepository employeeNomineeRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
        _employeePersonalRepository = employeePersonalRepository;
        _employeePayrollRepository = employeePayrollRepository;
        _employeeVerificationRepository = employeeVerificationRepository;
        _unitRepository = unitRepository;
        _departmentRepository = departmentRepository;
        _sectionRepository = sectionRepository;
        _shiftRepository = shiftRepository;
        _employeeSalaryAccountRepository = employeeSalaryAccountRepository;
        _employeeNomineeRepository = employeeNomineeRepository;
        _employeeNatureRepository = employeeNatureRepository;
        _employeeEmploymentRepository = employeeEmploymentRepository;
        _employeeDocumentRepository = employeeDocumentRepository;
    }

    public async Task<string> Handle(CreateCandidateEntryCommand command, CancellationToken cancellationToken = default)
    {
        // Use the provided date (caller should pass UTC DateTime)
        var today = DateTime.UtcNow;
        var next = await _employeeMasterRepository.GetNextEnrollmentIdAsync(today, cancellationToken: cancellationToken);

        // Create EmployeeMaster
        var employeeMaster = new HrmEmployeeMaster
        {
            EnrollmentId = next,
            EmployeeNameBangla = command.EmployeeNameBangla ?? string.Empty,
            EmployeeType = command.EmployeeType.ToString(),
            Status = EmployeeStatus.CandidateEntry.ToString(),
            IsActive = true
        };

        // Save EmployeeMaster first
        var employeeId = await _employeeMasterRepository.AddAsync(employeeMaster, cancellationToken);

        // Create EmployeePersonal
        var employeePersonal = new HrmEmployeePersonal
        {
            EmployeeId = employeeId,
            DateOfBirth = command.DateOfBirth,
            Gender = command.Gender.ToString(),
            BloodGroup = command.BloodGroup?.ToString(),
            GuardianType = command.GuardianType,
            GuardianNameBangla = command.GuardianNameBangla,
            MotherNameBangla = command.MotherNameBangla,
            FatherNameBangla = command.FatherNameBangla,
            MobileNumber = command.MobileNumber,
            EmployeeReference = command.EmployeeReference,
            ReferenceType = command.ReferenceType?.ToString(),
            ReferencePersonId = command.ReferencePersonId,
            ReferenceMobileNumber = command.ReferenceMobileNumber,
            Relationship = command.Relationship?.ToString(),
            Religion = command.Religion.ToString(),
            PermanentVillageAreaRoad = command.PermanentVillageAreaRoad,
            PermanentPostOffice = command.PermanentPostOffice,
            PermanentThanaId = command.PermanentThanaId,
            PermanentDistrictId = command.PermanentDistrictId,
            PermanentDivisionId = command.PermanentDivisionId,
            PresentVillageAreaRoad = command.PresentVillageAreaRoad,
            PresentPostOffice = command.PresentPostOffice,
            PresentThanaId = command.PresentThanaId,
            PresentDistrictId = command.PresentDistrictId,
            PresentDivisionId = command.PresentDivisionId,
            IsActive = true
        };

        // Save EmployeePersonal
        await _employeePersonalRepository.AddAsync(employeePersonal, cancellationToken);

        var employeeVerification = new HrmEmployeeVerification
        {
            EmployeeId = employeeId,
            SecurityClearanceBy = command.SecurityClearanceBy,
            SecurityClearanceDate = command.SecurityClearanceDate,
            EnrolledBy = command.EnrolledBy,
            EnrolledDate = command.EnrolledDate,
            BiometricEnrolledBy = command.BiometricEnrolledBy,
            BiometricEnrolledDate = command.BiometricEnrolledDate,
            IsActive = true
        };

        await _employeeVerificationRepository.AddAsync(employeeVerification, cancellationToken);

        var employeeEmployment = new HrmEmployeeEmployment
        {
            EmployeeId = employeeId,
            UnitId = command.UnitId,
            DesignationId = command.DesignationId,
            JoiningDate = command.JoiningDate,
            IsActive = true
        };

        await _employeeEmploymentRepository.AddAsync(employeeEmployment, cancellationToken);

        return employeeId;
    }


    public async Task<string> Handle(string employeeId, UpdateCandidateEntryCommand command, CancellationToken cancellationToken = default)
    {
        var employeee = await _employeeMasterRepository.GetByIdAsync(employeeId, cancellationToken: cancellationToken);

        if (employeee == null)
            throw new KeyNotFoundException($"Employee with ID {employeeId} not found");

        employeee.EmployeeNameBangla = command.EmployeeNameBangla ?? string.Empty;
            employeee.EmployeeType = command.EmployeeType.ToString();
        employeee.Status = EmployeeStatus.CandidateEntry.ToString();
        employeee.IsActive = true;
        // Save EmployeeMaster first
        await _employeeMasterRepository.UpdateAsync(employeee, cancellationToken);

        if (employeee.Personal != null)
        {
            employeee.Personal.EmployeeId = employeeId;
            employeee.Personal.DateOfBirth = command.DateOfBirth;
            employeee.Personal.Gender = command.Gender.ToString();
            employeee.Personal.BloodGroup = command.BloodGroup?.ToString();
            employeee.Personal.GuardianType = command.GuardianType;
            employeee.Personal.GuardianNameBangla = command.GuardianNameBangla;
            employeee.Personal.MotherNameBangla = command.MotherNameBangla;
            employeee.Personal.FatherNameBangla = command.FatherNameBangla;
            employeee.Personal.MobileNumber = command.MobileNumber;
            employeee.Personal.EmployeeReference = command.EmployeeReference;
            employeee.Personal.ReferenceType = command.ReferenceType?.ToString();
            employeee.Personal.ReferencePersonId = command.ReferencePersonId;
            employeee.Personal.ReferenceMobileNumber = command.ReferenceMobileNumber;
            employeee.Personal.Relationship = command.Relationship?.ToString();
            employeee.Personal.Religion = command.Religion.ToString();
            employeee.Personal.PermanentVillageAreaRoad = command.PermanentVillageAreaRoad;
            employeee.Personal.PermanentPostOffice = command.PermanentPostOffice;
            employeee.Personal.PermanentThanaId = command.PermanentThanaId;
            employeee.Personal.PermanentDistrictId = command.PermanentDistrictId;
            employeee.Personal.PermanentDivisionId = command.PermanentDivisionId;
            employeee.Personal.PresentVillageAreaRoad = command.PresentVillageAreaRoad;
            employeee.Personal.PresentPostOffice = command.PresentPostOffice;
            employeee.Personal.PresentThanaId = command.PresentThanaId;
            employeee.Personal.PresentDistrictId = command.PresentDistrictId;
            employeee.Personal.PresentDivisionId = command.PresentDivisionId;
            employeee.Personal.IsActive = true;

            await _employeePersonalRepository.UpdateAsync(employeee.Personal, cancellationToken);
        }
        if (employeee.Verification != null)
        {
            employeee.Verification.EmployeeId = employeeId;
            employeee.Verification.SecurityClearanceBy = command.SecurityClearanceBy;
            employeee.Verification.SecurityClearanceDate = command.SecurityClearanceDate;
            employeee.Verification.EnrolledBy = command.EnrolledBy;
            employeee.Verification.EnrolledDate = command.EnrolledDate;
            employeee.Verification.BiometricEnrolledBy = command.BiometricEnrolledBy;
            employeee.Verification.BiometricEnrolledDate = command.BiometricEnrolledDate;
            employeee.Verification.IsActive = true;
            await _employeeVerificationRepository.UpdateAsync(employeee.Verification, cancellationToken);
        }
        if (employeee.Employment != null)
        {
            employeee.Employment.EmployeeId = employeeId;
            employeee.Employment.UnitId = command.UnitId;
            employeee.Employment.DesignationId = command.DesignationId;
            employeee.Employment.JoiningDate = command.JoiningDate;
            employeee.Employment.IsActive = true;
            await _employeeEmploymentRepository.UpdateAsync(employeee.Employment, cancellationToken);
        }
        return employeeId;
    }

    public async Task<string> Handle(CreateEmployeeHRExecutiveCommand command, CancellationToken cancellationToken = default)
    {
        var employeeMaster = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);

        if (employeeMaster == null || employeeMaster.EnrollmentId != command.EmployeeEnrollmentId)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        employeeMaster.EmployeeCode = command.EmployeeCode;
        employeeMaster.EmployeeName = command.EmployeeName ?? string.Empty;
        employeeMaster.EmployeeType = command.EmployeeNatureId.ToString();
        employeeMaster.Status = EmployeeStatus.HRExecutive.ToString();

        await _employeeMasterRepository.UpdateAsync(employeeMaster, cancellationToken);
        // Validate related entities exist
        await ValidateRelatedEntities(command.UnitId, command.DepartmentId, command.SectionId, command.ShiftId, null, cancellationToken);

        if (employeeMaster.Employment is null)
        {
            var employeeEmployment = new HrmEmployeeEmployment
            {
                EmployeeId = employeeMaster.Id,
                UnitId = command.UnitId,
                SubunitId = command.SubunitId,
                DepartmentId = command.DepartmentId,
                SectionId = command.SectionId,
                ShiftId = command.ShiftId,
                CellId = command.CellId,
                DesignationId = command.DesignationId,
                GradeId = command.GradeId,
                WeeklyOffDay = command.Holiday.ToString(),
                EmployeeCategoryId = command.EmployeeTypeId,
                ProbationPeriod = command.ProbationPeriod,
                ReportingTo = command.ReportingTo,
                JoiningDate = command.JoiningDate,
                ProcessingGroupId = command.ProcessingGroupId,
                IsActive = true
            };

            await _employeeEmploymentRepository.AddAsync(employeeEmployment, cancellationToken);
        }
        else
        {
            employeeMaster.Employment.EmployeeId = employeeMaster.Id;
            employeeMaster.Employment.UnitId = command.UnitId;
            employeeMaster.Employment.SubunitId = command.SubunitId;
            employeeMaster.Employment.DepartmentId = command.DepartmentId;
            employeeMaster.Employment.SectionId = command.SectionId;
            employeeMaster.Employment.ShiftId = command.ShiftId;
            employeeMaster.Employment.CellId = command.CellId;
            employeeMaster.Employment.DesignationId = command.DesignationId;
            employeeMaster.Employment.GradeId = command.GradeId;
            employeeMaster.Employment.WeeklyOffDay = command.Holiday.ToString();
            employeeMaster.Employment.EmployeeCategoryId = command.EmployeeTypeId;
            //employeeMaster.Employment.EmployeeNatureId = command.EmployeeNatureId;
            employeeMaster.Employment.ProbationPeriod = command.ProbationPeriod;
            employeeMaster.Employment.ReportingTo = command.ReportingTo;
            employeeMaster.Employment.JoiningDate = command.JoiningDate;
            employeeMaster.Employment.ProcessingGroupId = command.ProcessingGroupId;
            employeeMaster.Employment.IsActive = true;

            await _employeeEmploymentRepository.UpdateAsync(employeeMaster.Employment, cancellationToken);
        }
        await UpsertPersonalInfo(command, employeeMaster, cancellationToken);

        await UpsertPayroll(command, employeeMaster, cancellationToken);

        await UpsertNominee(command, employeeMaster, cancellationToken);

        if (employeeMaster.Documents is null || employeeMaster.Documents.Count == 0)
        {
            AddEmployeeDocument(command.Documents, employeeMaster, cancellationToken).Wait(cancellationToken);
        }

        return command.EmployeeId;
    }

    private async Task UpsertNominee(CreateEmployeeHRExecutiveCommand command, HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken)
    {

        var employeeNominee = new HrmEmployeeNominee
        {
            EmployeeId = employeeMaster.Id,
            NomineeName = command.NomineeName,
            Relationship = command.NomineeRelation,
            //DateOfBirth = command.DateOfBirth,
            NidNo = command.NomineeID,
            MobileNo = command.NomineeMobileNumber,
            //Address = command.Address,
            //NominationPercentage = command.NominationPercentage,
            IsActive = true
        };
        await _employeeNomineeRepository.AddAsync(employeeNominee, cancellationToken);
    }

    private async Task UpsertPayroll(CreateEmployeeHRExecutiveCommand command, HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken)
    {
        if (employeeMaster.Payroll is null)
        {
            var salaryAccount = new HrmEmployeeSalaryAccount
            {
                EmployeeId = employeeMaster.Id,
                BankingId = command.BankingId,
                AccountName = command.AccountName,
                AccountNo = command.AccountNo,
                RoutingNo = command.RoutingNo,
                BranchName = command.BranchName,
                AccountType = command.AccountType,
                IsActive = true
            };

            var employeePayroll = new HrmEmployeePayroll
            {
                EmployeeId = employeeMaster.Id,
                ProposedSalary = command.ProposedMonthlySalary,
                GrossSalary = command.GrossSalary,
                BankPortion = command.BankPortion,
                CashPortion = command.CashPortion,
                OtherAllowance = JsonSerializer.Serialize(command.OtherAllowance),
                TINNo = command.TinNumber,
                Tax = command.Tax,
                IsActive = true,
                SalaryAccount = salaryAccount
            };
            await _employeePayrollRepository.AddAsync(employeePayroll, cancellationToken);
        }
        else
        {
            employeeMaster.Payroll.EmployeeId = employeeMaster.Id;
            employeeMaster.Payroll.ProposedSalary = command.ProposedMonthlySalary;
            employeeMaster.Payroll.GrossSalary = command.GrossSalary;
            employeeMaster.Payroll.BankPortion = command.BankPortion;
            employeeMaster.Payroll.CashPortion = command.CashPortion;
            employeeMaster.Payroll.OtherAllowance = JsonSerializer.Serialize(command.OtherAllowance);
            employeeMaster.Payroll.TINNo = command.TinNumber;
            employeeMaster.Payroll.Tax = command.Tax;
            employeeMaster.Payroll.IsActive = true;

            if (employeeMaster.Payroll.SalaryAccount == null)
            {
                var salaryAccount = new HrmEmployeeSalaryAccount
                {
                    EmployeeId = employeeMaster.Id,
                    BankingId = command.BankingId,
                    AccountName = command.AccountName,
                    AccountNo = command.AccountNo,
                    RoutingNo = command.RoutingNo,
                    BranchName = command.BranchName,
                    AccountType = command.AccountType,
                    IsActive = true
                };
                await _employeeSalaryAccountRepository.AddAsync(salaryAccount, cancellationToken);

                employeeMaster.Payroll.SalaryAccountId = salaryAccount.Id;
            }
            else
            {
                employeeMaster.Payroll.SalaryAccount.EmployeeId = employeeMaster.Id;
                employeeMaster.Payroll.SalaryAccount.BankingId = command.BankingId;
                employeeMaster.Payroll.SalaryAccount.AccountName = command.AccountName;
                employeeMaster.Payroll.SalaryAccount.AccountNo = command.AccountNo;
                employeeMaster.Payroll.SalaryAccount.RoutingNo = command.RoutingNo;
                employeeMaster.Payroll.SalaryAccount.BranchName = command.BranchName;
                employeeMaster.Payroll.SalaryAccount.AccountType = command.AccountType;
            }

            await _employeePayrollRepository.UpdateAsync(employeeMaster.Payroll, cancellationToken);
        }
    }

    private async Task UpsertPersonalInfo(CreateEmployeeHRExecutiveCommand command, HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken)
    {
        if (employeeMaster.Personal is null)
        {
            var employeePersonal = new HrmEmployeePersonal
            {
                EmployeeId = employeeMaster.Id,
                MotherName = command.MotherName,
                FatherName = command.FatherName,
                MobileNumber = command.MobileNumber,
                IsActive = true
            };

            // Save EmployeePersonal
            await _employeePersonalRepository.AddAsync(employeePersonal, cancellationToken);
        }
        else
        {
            var personal = employeeMaster.Personal;
            personal.MotherName = command.MotherName;
            personal.FatherName = command.FatherName;
            personal.MobileNumber = command.MobileNumber;
            await _employeePersonalRepository.UpdateAsync(personal, cancellationToken);
        }
    }

    public async Task<string> Handle(CreateBiometricCommand command, CancellationToken cancellationToken = default)
    {
        var employeeMaster = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);

        if (employeeMaster == null || employeeMaster.EnrollmentId != command.EmployeeEnrollmentId)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        //employeeMaster.CardNo = command.CardNo;
        employeeMaster.Status = EmployeeStatus.Biometric.ToString();
        await AddEmployeeDocument(command.Documents, employeeMaster, cancellationToken);
        
        await _employeeMasterRepository.UpdateAsync(employeeMaster, cancellationToken);

        return command.EmployeeId;
    }

    public async Task<List<string>> Handle(List<CreateDirectorReviewCommand> commands, CancellationToken cancellationToken = default)
    {
        var payrolls = new List<HrmEmployeePayroll>();
        var employees = new List<HrmEmployeeMaster>();
        foreach (var command in commands)
        {
            var (payroll, employee) = await DirectorsReviewMap(command, cancellationToken);
            if (payroll is null)
                throw new InvalidOperationException($"Payroll for employee with ID {command.EmployeeId} not found");
            payrolls.Add(payroll);
            employees.Add(employee);
        }
        var result = await _employeePayrollRepository.UpdateRangeAsync(payrolls, cancellationToken);
        await _employeeMasterRepository.UpdateRangeAsync(employees, cancellationToken);
        return result.ToList();
    }

    public async Task<string> Handle(CreateITActivationCommand command, CancellationToken cancellationToken = default)
    {
        var employeeMaster = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);

        if (employeeMaster == null || employeeMaster.EnrollmentId != command.EmployeeEnrollmentId)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        employeeMaster.Status = EmployeeStatus.ITActivation.ToString();
        await _employeeMasterRepository.UpdateAsync(employeeMaster, cancellationToken);

        return command.EmployeeId;
    }

    private async Task<(HrmEmployeePayroll?, HrmEmployeeMaster)> DirectorsReviewMap(CreateDirectorReviewCommand command, CancellationToken cancellationToken = default)
    {
        var existingEmployee = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (existingEmployee is null)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        var payroll = existingEmployee.Payroll;
        if (payroll != null)
        {
            payroll.ProposedSalary = command.ProposedMonthlySalary;
            payroll.GrossSalary = command.ProposedMonthlySalary;
        }

        existingEmployee.Status = EmployeeStatus.DirectorReview.ToString();
        return (payroll, existingEmployee);
    }

    private async Task AddEmployeeDocument(List<EmployeeDocumentDto>? documents,
        HrmEmployeeMaster employeeMaster, CancellationToken cancellationToken)
    {
        var employeeDocuments = (documents ?? []).Select(item => new HrmEmployeeDocument
        {
            EmployeeId = employeeMaster.Id,
            DocumentNo = item.DocumentNo,
            DocumentType = item.DocumentType.ToString(),
            IssueDate = item.IssueDate,
            ExpiryDate = item.ExpiryDate,
            FileName = item.FileName,
            FilePath = item.FilePath,
            IsActive = true
        }).ToList();

        if (employeeDocuments.Any())
            await _employeeDocumentRepository.AddRangeAsync(employeeDocuments, cancellationToken);
    }

    private async Task ValidateRelatedEntities(
        string unitId, 
        string departmentId,
        string sectionId, 
        string? shiftId,
        string? employeeNatureId,
        //string locationId, 
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(unitId))
        {
            var unitExists = await _unitRepository.ExistsAsync(unitId, cancellationToken);
            if (!unitExists)
                throw new KeyNotFoundException($"Unit with ID {unitId} not found");
        }
        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            var departmentExists = await _departmentRepository.ExistsAsync(departmentId, cancellationToken);
            if (!departmentExists)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found");
        }
        if (!string.IsNullOrWhiteSpace(sectionId))
        {
            var sectionExists = await _sectionRepository.ExistsAsync(sectionId, cancellationToken);
            if (!sectionExists)
                throw new KeyNotFoundException($"Section with ID {sectionId} not found");
        }

        if (!string.IsNullOrWhiteSpace(shiftId))
        {
            var shiftExists = await _shiftRepository.ExistsAsync(shiftId, cancellationToken);
            if (!shiftExists)
                throw new KeyNotFoundException($"Shift with ID {shiftId} not found");
        }

        if (!string.IsNullOrWhiteSpace(employeeNatureId))
        {
            var employeeNatureExists = await _employeeNatureRepository.ExistsAsync(employeeNatureId, cancellationToken);
            if (!employeeNatureExists)
                throw new KeyNotFoundException($"Employee nature with ID {employeeNatureId} not found");
        }
    }

}
