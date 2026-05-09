using NZ.HRM.Application.Employees.Commands.CreateCompleteEmployee;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.Employees.Handlers;

public class CreateCompleteEmployeeCommandHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;
    private readonly IEmployeePersonalRepository _employeePersonalRepository;
    private readonly IEmployeeVerificationRepository _employeeVerificationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IGradeRepository _gradeRepository;

    public CreateCompleteEmployeeCommandHandler(
        IEmployeeMasterRepository employeeMasterRepository,
        IEmployeePersonalRepository employeePersonalRepository,
        IEmployeeVerificationRepository employeeVerificationRepository,
        ICompanyRepository companyRepository,
        IDepartmentRepository departmentRepository,
        ISectionRepository sectionRepository,
        IGradeRepository gradeRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
        _employeePersonalRepository = employeePersonalRepository;
        _employeeVerificationRepository = employeeVerificationRepository;
        _companyRepository = companyRepository;
        _departmentRepository = departmentRepository;
        _sectionRepository = sectionRepository;
        _gradeRepository = gradeRepository;
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
            command.GradeId, 
            cancellationToken);

        // Create EmployeeMaster
        var employeeMaster = new EmployeeMaster
        {
            EmployeeCode = command.EmployeeCode,
            EmployeeNameEnglish = command.EmployeeNameEnglish,
            EmployeeNameBangla = command.EmployeeNameBangla,
            CompanyId = command.CompanyId,
            DepartmentId = command.DepartmentId,
            SectionId = command.SectionId,
            GradeId = command.GradeId,
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

        // Save EmployeeMaster first
        var employeeId = await _employeeMasterRepository.AddAsync(employeeMaster, cancellationToken);

        // Create EmployeePersonal
        var employeePersonal = new EmployeePersonal
        {
            EmployeeId = employeeId,
            EmployeeCode = command.EmployeeCode,
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
            TinNumber = command.TinNumber,
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

        // Save EmployeePersonal
        await _employeePersonalRepository.AddAsync(employeePersonal, cancellationToken);

        var employeeVerification = new EmployeeVerification
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

    private async Task ValidateRelatedEntities(
        string companyId, 
        string departmentId, 
        string sectionId, 
        string gradeId, 
        CancellationToken cancellationToken)
    {
        var companyExists = await _companyRepository.ExistsAsync(companyId, cancellationToken);
        if (!companyExists)
            throw new KeyNotFoundException($"Company with ID {companyId} not found");

        var departmentExists = await _departmentRepository.ExistsAsync(departmentId, cancellationToken);
        if (!departmentExists)
            throw new KeyNotFoundException($"Department with ID {departmentId} not found");

        var sectionExists = await _sectionRepository.ExistsAsync(sectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {sectionId} not found");

        var gradeExists = await _gradeRepository.ExistsAsync(gradeId, cancellationToken);
        if (!gradeExists)
            throw new KeyNotFoundException($"Grade with ID {gradeId} not found");
    }
}
