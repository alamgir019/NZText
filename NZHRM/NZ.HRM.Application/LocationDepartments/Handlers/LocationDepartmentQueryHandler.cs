using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.LocationDepartments.Queries.GetAllLocationDepartments;
using NZ.HRM.Application.LocationDepartments.Queries.GetLocationDepartmentById;

namespace NZ.HRM.Application.LocationDepartments.Handlers;

public class LocationDepartmentQueryHandler
{
    private readonly ILocationDepartmentRepository _locationDepartmentRepository;

    public LocationDepartmentQueryHandler(ILocationDepartmentRepository locationDepartmentRepository)
    {
        _locationDepartmentRepository = locationDepartmentRepository;
    }

    public async Task<List<LocationDepartmentDto>> Handle(GetAllLocationDepartmentsQuery query, CancellationToken cancellationToken = default)
    {
        var mappings = await _locationDepartmentRepository.GetAllAsync(
            query.IncludeInactive,
            query.LocationId,
            query.DepartmentId,
            cancellationToken);

        return mappings.Select(m => new LocationDepartmentDto
        {
            Id = m.Id,
            //LocationId = m.LocationId,
            //LocationName = m.Location?.LocationName ?? string.Empty,
            DepartmentId = m.DepartmentId,
            DepartmentName = m.Department?.DepartmentName ?? string.Empty,
            CreatedOn = m.CreatedOn,
            CreatedBy = m.CreatedBy,
            UpdatedOn = m.UpdatedOn,
            UpdatedBy = m.UpdatedBy,
            IsActive = m.IsActive
        }).ToList();
    }

    public async Task<LocationDepartmentDetailDto?> Handle(GetLocationDepartmentByIdQuery query, CancellationToken cancellationToken = default)
    {
        var mapping = await _locationDepartmentRepository.GetByIdAsync(query.Id, cancellationToken);
        if (mapping == null)
            return null;

        return new LocationDepartmentDetailDto
        {
            Id = mapping.Id,
            //LocationId = mapping.LocationId,
            //LocationName = mapping.Location?.LocationName ?? string.Empty,
            DepartmentId = mapping.DepartmentId,
            DepartmentName = mapping.Department?.DepartmentName ?? string.Empty,
            CreatedOn = mapping.CreatedOn,
            CreatedBy = mapping.CreatedBy,
            UpdatedOn = mapping.UpdatedOn,
            UpdatedBy = mapping.UpdatedBy,
            IsActive = mapping.IsActive
        };
    }
}
