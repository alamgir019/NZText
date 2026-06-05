using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.CreateMedicalFitnessCheck;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.DeleteMedicalFitnessCheck;
using NZ.HRM.Application.MedicalFitnessChecks.Commands.UpdateMedicalFitnessCheck;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

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

    public async Task<string> Handle(CreateMedicalFitnessCheckCommand command, CancellationToken cancellationToken = default)
    {
        var employeeExists = await _employeeMasterRepository.ExistsAsync(command.EmployeeId, cancellationToken);
        if (!employeeExists)
            throw new KeyNotFoundException($"Employee with ID {command.EmployeeId} not found");

        var medicalFitnessCheck = new HrmMedicalFitnessCheck
        {
            EmployeeId = command.EmployeeId,
            EnrollmentId = command.EnrollmentId,
            //BloodGroup = command.BloodGroup,
            HeightCm = command.HeightCm,
            WeightKg = command.WeightKg,
            PhysicalExaminationDataJson = command.PhysicalExaminationDataJson,
            IsFit = command.IsFit,
            Remarks = command.Remarks,
            ExaminedByDoctor = command.ExaminedByDoctor,
            ExaminationDateTime = command.ExaminationDateTime,
            IsActive = true
        };

        return await _repository.AddAsync(medicalFitnessCheck, cancellationToken);
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
            //BloodGroup = command.BloodGroup,
            HeightCm = command.HeightCm,
            WeightKg = command.WeightKg,
            PhysicalExaminationDataJson = command.PhysicalExaminationDataJson,
            IsFit = command.IsFit,
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
