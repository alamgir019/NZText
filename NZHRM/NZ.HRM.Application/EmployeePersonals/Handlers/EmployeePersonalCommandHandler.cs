using NZ.HRM.Application.EmployeePersonals.Commands.CreateEmployeePersonal;
using NZ.HRM.Application.EmployeePersonals.Commands.DeleteEmployeePersonal;
using NZ.HRM.Application.EmployeePersonals.Commands.UpdateEmployeePersonal;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.EmployeePersonals.Handlers;

public class EmployeePersonalCommandHandler
{
    private readonly IEmployeePersonalRepository _employeePersonalRepository;
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public EmployeePersonalCommandHandler(
        IEmployeePersonalRepository employeePersonalRepository,
        IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeePersonalRepository = employeePersonalRepository;
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<string> Handle(CreateEmployeePersonalCommand command, CancellationToken cancellationToken = default)
    {
        // Validate that employee exists
        var employeeExists = await _employeeMasterRepository.ExistsAsync(command.EmployeeId, cancellationToken);
        if (!employeeExists)
        {
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");
        }

        // Check if personal info already exists for this employee
        var alreadyExists = await _employeePersonalRepository.ExistsForEmployeeAsync(command.EmployeeId, cancellationToken);
        if (alreadyExists)
        {
            throw new ArgumentException($"Personal information already exists for employee {command.EmployeeId}");
        }

        // Get employee code from EmployeeMaster
        var employee = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);

        var employeePersonal = new EmployeePersonal
        {
            EmployeeId = command.EmployeeId,
            EmployeeCode = employee?.EmployeeCode ?? string.Empty,
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
            IsActive = true
        };

        return await _employeePersonalRepository.AddAsync(employeePersonal, cancellationToken);
    }

    public async Task Handle(UpdateEmployeePersonalCommand command, CancellationToken cancellationToken = default)
    {
        var employeePersonal = await _employeePersonalRepository.GetByIdAsync(command.Id, cancellationToken);

        if (employeePersonal == null)
            throw new KeyNotFoundException($"Employee personal information with ID {command.Id} not found");

        // Validate that employee exists
        var employeeExists = await _employeeMasterRepository.ExistsAsync(command.EmployeeId, cancellationToken);
        if (!employeeExists)
        {
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");
        }

        employeePersonal.EmployeeId = command.EmployeeId;
        employeePersonal.DateOfBirth = command.DateOfBirth;
        employeePersonal.Gender = command.Gender;
        employeePersonal.MaritalStatus = command.MaritalStatus;
        employeePersonal.MobileNumber = command.MobileNumber;
        employeePersonal.EmailAddress = command.EmailAddress;
        employeePersonal.DocumentType = command.DocumentType;
        employeePersonal.DocumentNumber = command.DocumentNumber;
        employeePersonal.BloodGroup = command.BloodGroup;
        employeePersonal.Religion = command.Religion;
        employeePersonal.Nationality = command.Nationality;
        employeePersonal.FatherNameEnglish = command.FatherNameEnglish;
        employeePersonal.FatherNameBangla = command.FatherNameBangla;
        employeePersonal.MotherNameEnglish = command.MotherNameEnglish;
        employeePersonal.MotherNameBangla = command.MotherNameBangla;
        employeePersonal.SpouseName = command.SpouseName;
        employeePersonal.SpouseMobile = command.SpouseMobile;
        employeePersonal.TinNumber = command.TinNumber;
        employeePersonal.EmployeeReference = command.EmployeeReference;

        await _employeePersonalRepository.UpdateAsync(employeePersonal, cancellationToken);
    }

    public async Task Handle(DeleteEmployeePersonalCommand command, CancellationToken cancellationToken = default)
    {
        var employeePersonal = await _employeePersonalRepository.GetByIdAsync(command.Id, cancellationToken);

        if (employeePersonal == null)
            throw new KeyNotFoundException($"Employee personal information with ID {command.Id} not found");

        // Soft delete
        employeePersonal.IsActive = false;
        await _employeePersonalRepository.UpdateAsync(employeePersonal, cancellationToken);

        // Or hard delete
        // await _employeePersonalRepository.DeleteAsync(employeePersonal, cancellationToken);
    }
}
