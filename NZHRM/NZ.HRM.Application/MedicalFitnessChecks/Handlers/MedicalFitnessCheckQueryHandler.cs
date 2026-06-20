using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetAllMedicalFitnessChecks;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessCheckById;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessHistoryByEmployeeId;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessReportByEmployeeId;
using NZ.HRM.Mapping.MedicalFitnessChecks;

namespace NZ.HRM.Application.MedicalFitnessChecks.Handlers;

public class MedicalFitnessCheckQueryHandler
{
    private readonly IMedicalFitnessCheckRepository _repository;
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public MedicalFitnessCheckQueryHandler(
        IMedicalFitnessCheckRepository repository,
        IEmployeeMasterRepository employeeMasterRepository)
    {
        _repository = repository;
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<List<MedicalFitnessCheckDto>> Handle(GetAllMedicalFitnessChecksQuery query, CancellationToken cancellationToken = default)
    {
        var checks = await _repository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return checks.Select(x => new MedicalFitnessCheckDto
        {
            Id = x.Id,
            EmployeeId = x.EmployeeId,
            TemporaryCandidateId = x.EnrollmentId,
            //BloodGroup = x.BloodGroup,
            HeightCm = x.HeightCm,
            WeightKg = x.WeightKg,
            PhysicalExaminationDataJson = x.PhysicalExaminationDataJson,
            IsFit = x.IsFit,
            Remarks = x.Remarks,
            ExaminedByDoctor = x.ExaminedByDoctor,
            ExaminationDateTime = x.ExaminationDateTime,
            CreatedOn = x.CreatedOn,
            CreatedBy = x.CreatedBy,
            UpdatedOn = x.UpdatedOn,
            UpdatedBy = x.UpdatedBy,
            IsActive = x.IsActive
        }).ToList();
    }

    public async Task<MedicalFitnessCheckDetailDto?> Handle(GetMedicalFitnessCheckByIdQuery query, CancellationToken cancellationToken = default)
    {
        var check = await _repository.GetByIdAsync(query.Id, cancellationToken);
        if (check == null)
            return null;

        return new MedicalFitnessCheckDetailDto
        {
            Id = check.Id,
            EmployeeId = check.EmployeeId,
            EnrollmentId = check.EnrollmentId,
            //BloodGroup = check.BloodGroup,
            HeightCm = check.HeightCm,
            WeightKg = check.WeightKg,
            PhysicalExaminationDataJson = check.PhysicalExaminationDataJson,
            IsFit = check.IsFit,
            Remarks = check.Remarks,
            ExaminedByDoctor = check.ExaminedByDoctor,
            ExaminationDateTime = check.ExaminationDateTime,
            CreatedOn = check.CreatedOn,
            CreatedBy = check.CreatedBy,
            UpdatedOn = check.UpdatedOn,
            UpdatedBy = check.UpdatedBy,
            IsActive = check.IsActive
        };
    }

    public async Task<MedicalFitnessReportDto?> Handle(GetMedicalFitnessReportByEmployeeIdQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        var medical = await _repository.GetLatestByEmployeeIdAsync(query.EmployeeId, cancellationToken);
        if (medical == null)
            return null;

        return employee.MapToMedicalFitnessReportDto(medical);
    }

    public async Task<List<MedicalFitnessHistoryDto>> Handle(GetMedicalFitnessHistoryByEmployeeIdQuery query, CancellationToken cancellationToken = default)
    {
        var checks = await _repository.GetByEmployeeIdAsync(query.EmployeeId, includeInactive: false, cancellationToken);

        return checks.Select(x => new MedicalFitnessHistoryDto
        {
            MedicalFitnessCheckId = x.Id,
            ExaminationDateTime = x.ExaminationDateTime,
            ExaminedByDoctor = x.ExaminedByDoctor,
            IsFit = x.IsFit
        }).ToList();
    }
}
