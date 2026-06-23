using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.CreateMedicalFitnessCheck;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.DeleteMedicalFitnessCheck;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.UpdateMedicalFitnessCheck;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.Application.MedicalFitnessChecks.Handlers;

public class MedicalFitnessCheckCommandHandler
{
    private readonly IMedicalFitnessCheckRepository _repository;
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public MedicalFitnessCheckCommandHandler(
        IMedicalFitnessCheckRepository repository,
        IEmployeeMasterRepository employeeMasterRepository)
    {
        _repository = repository;
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<(HrmMedicalFitnessCheck, HrmEmployeeMaster)> MedicalFitnessMap(CreateMedicalFitnessCheckCommand command, CancellationToken cancellationToken = default)
    {
        var existingEmployee = await _employeeMasterRepository.GetByIdAsync(command.EmployeeId, cancellationToken);
        if (existingEmployee is null)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        var medicalFitnessCheck = new HrmMedicalFitnessCheck
        {
            EmployeeId = command.EmployeeId,
            EnrollmentId = command.EnrollmentId,
            IdentificationSign = command.IdentificationSign,
            Fitness = command.Fitness.ToString(),
            Remarks = command.Remarks,
            ExaminedByDoctor = command.ExaminedByDoctor,
            ExaminationDateTime = command.ExaminationDateTime,
            IsActive = true
        };

        existingEmployee.Status = "Medical";
        //await _employeeMasterRepository.UpdateAsync(existingEmployee, cancellationToken);
        return (medicalFitnessCheck, existingEmployee);
    }

    public async Task<List<string>> Handle(List<CreateMedicalFitnessCheckCommand> commands, CancellationToken cancellationToken = default)
    {
        var medicals = new List<HrmMedicalFitnessCheck>();
        var employees = new List<HrmEmployeeMaster>();
        foreach (var command in commands)
        {
            var (medical, employee) = await MedicalFitnessMap(command, cancellationToken);
            medicals.Add(medical);
            employees.Add(employee);
        }
        var result = await _repository.AddRangeAsync(medicals, cancellationToken);
        await _employeeMasterRepository.UpdateRangeAsync(employees, cancellationToken);
        return result.ToList();
    }

    public async Task Handle(UpdateMedicalFitnessCheckCommand command, CancellationToken cancellationToken = default)
    {
        var employeeExists = await _employeeMasterRepository.ExistsAsync(command.EmployeeId, cancellationToken);
        if (!employeeExists)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        var newVersion = new HrmMedicalFitnessCheck
        {
            EmployeeId = command.EmployeeId,
            EnrollmentId = command.EnrollmentId,
            IdentificationSign = command.IdentificationSign,
            Fitness = command.Fitness.ToString(),
            Remarks = command.Remarks,
            ExaminedByDoctor = command.ExaminedByDoctor,
            ExaminationDateTime = command.ExaminationDateTime,
            IsActive = true
        };

        await _repository.AddAsync(newVersion, cancellationToken);
    }

    public async Task Handle(DeleteMedicalFitnessCheckCommand command, CancellationToken cancellationToken = default)
    {
        var medicalFitnessCheck = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (medicalFitnessCheck == null)
            throw new KeyNotFoundException($"Medical fitness check with ID {command.Id} not found");

        medicalFitnessCheck.IsActive = false;
        await _repository.UpdateAsync(medicalFitnessCheck, cancellationToken);
    }
}
