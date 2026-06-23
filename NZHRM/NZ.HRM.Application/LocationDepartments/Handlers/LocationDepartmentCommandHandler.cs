using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Interface;
using NZ.HRM.Application.LocationDepartments.Commands.CreateLocationDepartment;
using NZ.HRM.Application.LocationDepartments.Commands.DeleteLocationDepartment;
using NZ.HRM.Application.LocationDepartments.Commands.UpdateLocationDepartment;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.LocationDepartments.Handlers;

public class LocationDepartmentCommandHandler
{
    private readonly ILocationDepartmentRepository _locationDepartmentRepository;
    private readonly ISubUnitRepository _locationRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public LocationDepartmentCommandHandler(
        ILocationDepartmentRepository locationDepartmentRepository,
        ISubUnitRepository locationRepository,
        IDepartmentRepository departmentRepository)
    {
        _locationDepartmentRepository = locationDepartmentRepository;
        _locationRepository = locationRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<string> Handle(CreateLocationDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var location = await _locationRepository.FindByIdAsync(command.LocationId);
        if (location == null)
            throw new KeyNotFoundException($"Location with ID {command.LocationId} not found");

        var departmentExists = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!departmentExists)
            throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");

        //var mapping = new LocationDepartment
        //{
        //    LocationId = command.LocationId,
        //    DepartmentId = command.DepartmentId,
        //    IsActive = true
        //};

        //return await _locationDepartmentRepository.AddAsync(mapping, cancellationToken);
        return string.Empty;
    }

    public async Task Handle(UpdateLocationDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var mapping = await _locationDepartmentRepository.GetByIdAsync(command.Id, cancellationToken);
        if (mapping == null)
            throw new KeyNotFoundException($"LocationDepartment with ID {command.Id} not found");

        var location = await _locationRepository.FindByIdAsync(command.LocationId);
        if (location == null)
            throw new KeyNotFoundException($"Location with ID {command.LocationId} not found");

        var departmentExists = await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken);
        if (!departmentExists)
            throw new KeyNotFoundException($"Department with ID {command.DepartmentId} not found");

        //mapping.LocationId = command.LocationId;
        mapping.DepartmentId = command.DepartmentId;

        await _locationDepartmentRepository.UpdateAsync(mapping, cancellationToken);
    }

    public async Task Handle(DeleteLocationDepartmentCommand command, CancellationToken cancellationToken = default)
    {
        var mapping = await _locationDepartmentRepository.GetByIdAsync(command.Id, cancellationToken);
        if (mapping == null)
            throw new KeyNotFoundException($"LocationDepartment with ID {command.Id} not found");

        mapping.IsActive = false;
        await _locationDepartmentRepository.UpdateAsync(mapping, cancellationToken);
    }
}
