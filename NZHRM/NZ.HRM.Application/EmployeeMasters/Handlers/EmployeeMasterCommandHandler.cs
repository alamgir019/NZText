using NZ.HRM.Application.EmployeeMasters.Commands.CreateEmployeeMaster;
using NZ.HRM.Application.EmployeeMasters.Commands.DeleteEmployeeMaster;
using NZ.HRM.Application.EmployeeMasters.Commands.UpdateEmployeeMaster;
using NZ.HRM.Application.Interface;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.EmployeeMasters.Handlers;

public class EmployeeMasterCommandHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IEmployeeNatureRepository _employeeNatureRepository;

    public EmployeeMasterCommandHandler(
        IEmployeeMasterRepository employeeMasterRepository,
        ICompanyRepository companyRepository,
        IDepartmentRepository departmentRepository,
        ISectionRepository sectionRepository,
        ILocationRepository locationRepository,
        IGradeRepository gradeRepository,
        IShiftRepository shiftRepository,
        IEmployeeNatureRepository employeeNatureRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
        _companyRepository = companyRepository;
        _departmentRepository = departmentRepository;
        _sectionRepository = sectionRepository;
        _locationRepository = locationRepository;
        _gradeRepository = gradeRepository;
        _shiftRepository = shiftRepository;
        _employeeNatureRepository = employeeNatureRepository;
    }

    public async Task<string> Handle(CreateEmployeeMasterCommand command, CancellationToken cancellationToken = default)
    {
        // Validate employee code uniqueness
        var codeExists = await _employeeMasterRepository.EmployeeCodeExistsAsync(command.EmployeeCode, cancellationToken);
        if (codeExists)
        {
            throw new ArgumentException($"Employee code '{command.EmployeeCode}' already exists");
        }

        // Validate related entities exist
        await ValidateRelatedEntities(command.CompanyId, command.DepartmentId, command.SectionId, command.LocationId, command.GradeId, command.ShiftId, command.EmployeeNatureId, cancellationToken);

        var employeeMaster = new EmployeeMaster
        {
            EmployeeCode = command.EmployeeCode,
            EmployeeNameEnglish = command.EmployeeNameEnglish,
            EmployeeNameBangla = command.EmployeeNameBangla,
            CompanyId = command.CompanyId,
            DepartmentId = command.DepartmentId,
            SectionId = command.SectionId,
            LocationId = command.LocationId,
            GradeId = command.GradeId,
            EmployeeType = command.EmployeeType,
            ShiftId = command.ShiftId,
            EmployeeNatureId = command.EmployeeNatureId,
            Holiday = command.Holiday,
            ProposedMonthlySalary = command.ProposedMonthlySalary,
            JoiningDate = command.JoiningDate,
            ConfirmationDate = command.ConfirmationDate,
            Status = EmployeeStatus.Draft,
            IsActive = true
        };

        return await _employeeMasterRepository.AddAsync(employeeMaster, cancellationToken);
    }

    public async Task Handle(UpdateEmployeeMasterCommand command, CancellationToken cancellationToken = default)
    {
        var employeeMaster = await _employeeMasterRepository.GetByIdAsync(command.Id, cancellationToken);

        if (employeeMaster == null)
            throw new KeyNotFoundException($"Employee with ID {command.Id} not found");

        // Validate employee code uniqueness (excluding current employee)
        var existingEmployee = await _employeeMasterRepository.GetByEmployeeCodeAsync(command.EmployeeCode, cancellationToken);
        if (existingEmployee != null && existingEmployee.Id != command.Id)
        {
            throw new ArgumentException($"Employee code '{command.EmployeeCode}' already exists");
        }

        // Validate related entities exist
        await ValidateRelatedEntities(command.CompanyId, command.DepartmentId, command.SectionId, command.LocationId, command.GradeId, command.ShiftId, command.EmployeeNatureId, cancellationToken);

        employeeMaster.EmployeeCode = command.EmployeeCode;
        employeeMaster.EmployeeNameEnglish = command.EmployeeNameEnglish;
        employeeMaster.EmployeeNameBangla = command.EmployeeNameBangla;
        employeeMaster.CompanyId = command.CompanyId;
        employeeMaster.DepartmentId = command.DepartmentId;
        employeeMaster.SectionId = command.SectionId;
        employeeMaster.LocationId = command.LocationId;
        employeeMaster.GradeId = command.GradeId;
        employeeMaster.EmployeeType = command.EmployeeType;
        employeeMaster.ShiftId = command.ShiftId;
        employeeMaster.EmployeeNatureId = command.EmployeeNatureId;
        employeeMaster.Holiday = command.Holiday;
        employeeMaster.ProposedMonthlySalary = command.ProposedMonthlySalary;
        employeeMaster.JoiningDate = command.JoiningDate;
        employeeMaster.ConfirmationDate = command.ConfirmationDate;
        employeeMaster.Status = command.Status;

        await _employeeMasterRepository.UpdateAsync(employeeMaster, cancellationToken);
    }

    public async Task Handle(DeleteEmployeeMasterCommand command, CancellationToken cancellationToken = default)
    {
        var employeeMaster = await _employeeMasterRepository.GetByIdAsync(command.Id, cancellationToken);

        if (employeeMaster == null)
            throw new KeyNotFoundException($"Employee with ID {command.Id} not found");

        // Soft delete
        employeeMaster.IsActive = false;
        employeeMaster.Status = EmployeeStatus.Inactive;
        await _employeeMasterRepository.UpdateAsync(employeeMaster, cancellationToken);

        // Or hard delete
        // await _employeeMasterRepository.DeleteAsync(employeeMaster, cancellationToken);
    }

    private async Task ValidateRelatedEntities(string companyId, string departmentId, string sectionId, string locationId, string gradeId, string shiftId, string employeeNatureId, CancellationToken cancellationToken)
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

        var location = await _locationRepository.FindByIdAsync(locationId);
        if (location == null || !location.IsActive)
            throw new KeyNotFoundException($"Location with ID {locationId} not found");

        var gradeExists = await _gradeRepository.ExistsAsync(gradeId, cancellationToken);
        if (!gradeExists)
            throw new KeyNotFoundException($"Grade with ID {gradeId} not found");

        var shiftExists = await _shiftRepository.ExistsAsync(shiftId, cancellationToken);
        if (!shiftExists)
            throw new KeyNotFoundException($"Shift with ID {shiftId} not found");

        var employeeNatureExists = await _employeeNatureRepository.ExistsAsync(employeeNatureId, cancellationToken);
        if (!employeeNatureExists)
            throw new KeyNotFoundException($"Employee nature with ID {employeeNatureId} not found");
    }
}
