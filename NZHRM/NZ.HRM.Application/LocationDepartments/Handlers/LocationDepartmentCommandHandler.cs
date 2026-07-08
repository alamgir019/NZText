using NZ.HRM.Application.LocationDepartments.Commands.CreateLocationDepartment;
using NZ.HRM.Application.LocationDepartments.Commands.DeleteLocationDepartment;
using NZ.HRM.Application.LocationDepartments.Commands.UpdateLocationDepartment;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.LocationDepartments.Handlers;

public class LocationDepartmentCommandHandler
{
    private readonly IComplexUnitDepartmentRepository _locationRepo;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitRepository _unitRepository;
    private readonly IGroupComplexRepository _complexRepository;

    public LocationDepartmentCommandHandler(
        IComplexUnitDepartmentRepository locationRepo,
        IDepartmentRepository departmentRepository,
        IUnitRepository unitRepository,
        IGroupComplexRepository complexRepository)
    {
        _locationRepo = locationRepo;
        _departmentRepository = departmentRepository;
        _unitRepository = unitRepository;
        _complexRepository = complexRepository;
    }

    public async Task<string> Handle(CreateLocationDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        // validate department, unit, complex
        var dept = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!dept) throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");

        var unit = await _unitRepository.ExistsAsync(command.UnitId, cancellationToken);
        if (!unit) throw new KeyNotFoundException($"Unit with ID {command.UnitId} not found");

        var complex = await _complexRepository.ExistsAsync(command.ComplexId, cancellationToken);
        if (!complex) throw new KeyNotFoundException($"Complex with ID {command.ComplexId} not found");

        var mapping = new MstDepartmentUnitComplex
        {
            DepartmentId = command.DepartmentId,
            UnitId = command.UnitId,
            ComplexId = command.ComplexId,
            IsActive = true
        };

        return await _locationRepo.AddAsync(mapping, cancellationToken);
    }

    public async Task Handle(UpdateLocationDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var mapping = await _locationRepo.GetByIdAsync(command.Id, cancellationToken);
        if (mapping == null) throw new KeyNotFoundException($"Mapping with ID {command.Id} not found");

        var dept = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!dept) throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");

        var unit = await _unitRepository.ExistsAsync(command.UnitId, cancellationToken);
        if (!unit) throw new KeyNotFoundException($"Unit with ID {command.UnitId} not found");

        var complex = await _complexRepository.ExistsAsync(command.ComplexId, cancellationToken);
        if (!complex) throw new KeyNotFoundException($"Complex with ID {command.ComplexId} not found");

        mapping.DepartmentId = command.DepartmentId;
        mapping.UnitId = command.UnitId;
        mapping.ComplexId = command.ComplexId;

        await _locationRepo.UpdateAsync(mapping, cancellationToken);
    }

    public async Task Handle(DeleteLocationDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var mapping = await _locationRepo.GetByIdAsync(command.Id, cancellationToken);
        if (mapping == null) throw new KeyNotFoundException($"Mapping with ID {command.Id} not found");

        // soft delete
        mapping.IsActive = false;
        await _locationRepo.UpdateAsync(mapping, cancellationToken);
    }
}
