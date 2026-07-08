using NZ.HRM.Application.LocationDepartments.Queries.GetAllLocationDepartments;
using NZ.HRM.Application.LocationDepartments.Queries.GetLocationDepartmentById;
using NZ.HRM.Application.Interfaces.Repositories;
using System.Linq;

namespace NZ.HRM.Application.LocationDepartments.Handlers;

public class LocationDepartmentQueryHandler
{
    private readonly IComplexUnitDepartmentRepository _locationRepo;

    public LocationDepartmentQueryHandler(IComplexUnitDepartmentRepository locationRepo)
    {
        _locationRepo = locationRepo;
    }

    public async Task<List<LocationDepartmentDto>> Handle(GetAllLocationDepartmentsQuery query, CancellationToken cancellationToken = default)
    {
        var list = await _locationRepo.GetAllAsync(query.IncludeInactive, query.ComplexId, query.UnitId, cancellationToken);
            return list.Select(m => new LocationDepartmentDto
        {
            Id = m.Id,
            ComplexId = m.ComplexId,
            ComplexName = m.Complex?.ComplexName ?? string.Empty,
            UnitId = m.UnitId,
            UnitName = m.Unit?.UnitName ?? string.Empty,
            DepartmentId = m.DepartmentId,
            DepartmentName = m.Department?.DepartmentName ?? string.Empty
        }).ToList();
    }

    public async Task<LocationDepartmentDetailDto?> Handle(GetLocationDepartmentByIdQuery query, CancellationToken cancellationToken = default)
    {
        var m = await _locationRepo.GetByIdAsync(query.Id, cancellationToken);
        if (m == null) return null;
        return new LocationDepartmentDetailDto
        {
            Id = m.Id,
            ComplexId = m.ComplexId,
            ComplexName = m.Complex?.ComplexName ?? string.Empty,
            UnitId = m.UnitId,
            UnitName = m.Unit?.UnitName ?? string.Empty,
            DepartmentId = m.DepartmentId,
            DepartmentName = m.Department?.DepartmentName ?? string.Empty,
            IsActive = m.IsActive
        };
    }
}
