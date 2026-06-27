using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Mapping.Employees;
using NZ.HRM.Utility.Enum;
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
    private readonly IGradeRepository _gradeRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IEmployeeNatureRepository _employeeNatureRepository;
    private readonly IEmployeeEmploymentRepository _employeeEmploymentRepository;
    private readonly IEmployeeDocumentRepository _employeeDocumentRepository;

    public EmployeeCommandHandler(
        IEmployeeMasterRepository employeeMasterRepository,
        IEmployeePersonalRepository employeePersonalRepository,
        IPayrollRepository employeePayrollRepository,
        IEmployeeVerificationRepository employeeVerificationRepository,
        IUnitRepository unitRepository,
        IDepartmentRepository departmentRepository,
        ISectionRepository sectionRepository,
        IGradeRepository gradeRepository,
        IShiftRepository shiftRepository,
        IEmployeeNatureRepository employeeNatureRepository,
        IEmployeeEmploymentRepository employeeEmploymentRepository,
        IEmployeeDocumentRepository employeeDocumentRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
        _employeePersonalRepository = employeePersonalRepository;
        _employeePayrollRepository = employeePayrollRepository;
        _employeeVerificationRepository = employeeVerificationRepository;
        _unitRepository = unitRepository;
        _departmentRepository = departmentRepository;
        _sectionRepository = sectionRepository;
        _gradeRepository = gradeRepository;
        _shiftRepository = shiftRepository;
        _employeeDocumentRepository = employeeDocumentRepository;
        _employeeNatureRepository = employeeNatureRepository;
        _employeeEmploymentRepository = employeeEmploymentRepository;
    }

    public async Task<string> Handle(CreateCompleteEmployeeCommand command, CancellationToken cancellationToken = default)
    {
        // Validate employee code uniqueness
        var codeExists = await _employeeMasterRepository.EmployeeCodeExistsAsync(command.EmployeeCode, cancellationToken);
        if (codeExists)
        {
            throw new ArgumentException($"Employee code '{command.EmployeeCode}' already exists");
        }

        // Validate related entities exist
        await ValidateRelatedEntities(
            command.CompanyId, 
            command.DepartmentId, 
            command.SectionId, 
            command.ShiftId,
            command.EmployeeNatureId,
            //command.GradeId, 
            cancellationToken);

        // Create EmployeeMaster
        var employeeMaster = EmployeeMapper.CreateCompleteEmployeeCommandToMaster(command);

        // Save EmployeeMaster first
        var employeeId = await _employeeMasterRepository.AddAsync(employeeMaster, cancellationToken);

        // Create EmployeePersonal
        var employeePersonal = EmployeeMapper.CreateCompleteEmployeeCommandToPersonal(command, employeeId);

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

        return employeeId;
    }


    public async Task<string> Handle(CreateCandidateEntryCommand command, CancellationToken cancellationToken = default)
    {
        // Validate employee enrollment ID uniqueness
        var enrollmentExists = await _employeeMasterRepository.EnrollmentCodeExistsAsync(command.EmployeeEnrollmentId, cancellationToken);
        if (enrollmentExists)
        {
            throw new ArgumentException($"Employee enrollment ID '{command.EmployeeEnrollmentId}' already exists");
        }

        // Validate related entities exist
        await ValidateRelatedEntities(
            command.UnitId,
            command.DepartmentId,
            command.SectionId,
            null,
            null,
            //command.LocationId,
            cancellationToken);

        // Create EmployeeMaster
        var employeeMaster = new HrmEmployeeMaster
        {
            EnrollmentId = command.EmployeeEnrollmentId,
            EmployeeNameBangla = command.EmployeeNameBangla ?? string.Empty,
            //UnitId = command.UnitId,
            //DepartmentId = command.DepartmentId,
            //SectionId = command.SectionId,
            //CellId = command.CellId,
            EmployeeType = command.EmployeeType.ToString(),
            //ProposedMonthlySalary = command.ProposedMonthlySalary,
            //JoiningDate = command.JoiningDate,
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
            //GuardianType = command.GuardianType.ToString(),
            //GuardianName = command.GuardianName,
            //MotherNameBangla = command.MotherNameBangla,
            //IdType = command.IDType,
            //IDNumber = command.IDNumber,
            EmployeeReference = command.EmployeeReference,
            ReferenceType = command.ReferenceType?.ToString(),
            ReferencePersonId = command.ReferencePersonId,
            ReferenceMobileNumber = command.ReferenceMobileNumber,
            Relationship = command.Relationship?.ToString(),
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
            JoiningDate = command.JoiningDate,
            IsActive = true
        };

        await _employeeEmploymentRepository.AddAsync(employeeEmployment, cancellationToken);

        return employeeId;
    }

    public async Task<string> Handle(CreateEmployeeHRExecutiveCommand command, CancellationToken cancellationToken = default)
    {
        var employeeMaster = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);

        if (employeeMaster == null)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        // Validate employee enrollment ID uniqueness (excluding current employee)
        var existingEmployee = await _employeeMasterRepository.GetByEmployeeCodeAsync(command.EmployeeEnrollmentId, cancellationToken);
        if (existingEmployee != null && existingEmployee.Id != command.EmployeeId)
        {
            throw new ArgumentException($"Employee enrollment ID '{command.EmployeeEnrollmentId}' already exists");
        }

        employeeMaster.Status = EmployeeStatus.HRExecutive.ToString();

        await _employeeMasterRepository.UpdateAsync(employeeMaster, cancellationToken);

        // Validate related entities exist
        await ValidateRelatedEntities(command.UnitId, command.DepartmentId, command.SectionId, command.ShiftId, command.EmployeeNatureId, cancellationToken);

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
                EmployeeNatureId = command.EmployeeNatureId,
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
            employeeMaster.Employment.EmployeeNatureId = command.EmployeeNatureId;
            employeeMaster.Employment.ProbationPeriod = command.ProbationPeriod;
            employeeMaster.Employment.ReportingTo = command.ReportingTo;
            employeeMaster.Employment.JoiningDate = command.JoiningDate;
            employeeMaster.Employment.ProcessingGroupId = command.ProcessingGroupId;
            employeeMaster.Employment.IsActive = true;

            await _employeeEmploymentRepository.UpdateAsync(employeeMaster.Employment, cancellationToken);
        }


        if (employeeMaster.Payroll is null)
        {
            var employeePayroll = new HrmEmployeePayroll
            {
                EmployeeId = employeeMaster.Id,
                ProposedSalary = command.ProposedMonthlySalary,
                GrossSalary = command.GrossSalary,
                BankPortion = command.BankPortion,
                CashPortion = command.CashPortion,
                OtherAllowance = JsonSerializer.Serialize(command.OtherAllowance),
                SalaryAccountId = command.SalaryAccountId,
                TINNo = command.TinNumber,
                Tax = command.Tax,
                IsActive = true
            };

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
            employeePayroll.SalaryAccount = salaryAccount;
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
            employeeMaster.Payroll.SalaryAccountId = command.SalaryAccountId;
            employeeMaster.Payroll.TINNo = command.TinNumber;
            employeeMaster.Payroll.Tax = command.Tax;
            employeeMaster.Payroll.IsActive = true;
            employeeMaster.Payroll.SalaryAccount!.BankingId = command.BankingId;
            employeeMaster.Payroll.SalaryAccount.AccountName = command.AccountName;
            employeeMaster.Payroll.SalaryAccount.AccountNo = command.AccountNo;
            employeeMaster.Payroll.SalaryAccount.RoutingNo = command.RoutingNo;
            employeeMaster.Payroll.SalaryAccount.BranchName = command.BranchName;
            employeeMaster.Payroll.SalaryAccount.AccountType = command.AccountType;

            await _employeePayrollRepository.UpdateAsync(employeeMaster.Payroll, cancellationToken);
        }

        if (employeeMaster.Documents is null || employeeMaster.Documents.Count == 0)
        {
            var employeeDocuments = (command.Documents ?? []).Select(item => new HrmEmployeeDocument
            {
                EmployeeId = employeeMaster.Id,
                DocumentNo = item.DocumentNo,
                DocumentType = item.DocumentType,
                IssueDate = item.IssueDate,
                ExpiryDate = item.ExpiryDate,
                FileName = item.FileName,
                FilePath = item.FilePath,
                IsActive = true
            }).ToList();
            await _employeeDocumentRepository.AddRangeAsync(employeeDocuments, cancellationToken);
        }

        return command.EmployeeId;
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

        //var locationExists = await _locationRepository.ExistsAsync(locationId, cancellationToken);
        //if (!locationExists)
        //    throw new KeyNotFoundException($"Location with ID {locationId} not found");
    }

}
