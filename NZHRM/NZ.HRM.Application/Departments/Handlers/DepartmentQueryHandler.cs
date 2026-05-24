using NZ.HRM.Application.Departments.Queries.GetAllDepartments;
using NZ.HRM.Application.Departments.Queries.GetDepartmentsByLocation;
using NZ.HRM.Application.Departments.Queries.GetDepartmentById;
using NZ.HRM.Application.Interface;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Departments.Handlers;

public class DepartmentQueryHandler
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly ILocationDepartmentRepository _locationDepartmentRepository;

    public DepartmentQueryHandler(
        IDepartmentRepository departmentRepository,
        ILocationRepository locationRepository,
        ILocationDepartmentRepository locationDepartmentRepository)
    {
        _departmentRepository = departmentRepository;
        _locationRepository = locationRepository;
        _locationDepartmentRepository = locationDepartmentRepository;
    }

    public async Task<List<DepartmentDto>> Handle(GetAllDepartmentsQuery query, CancellationToken cancellationToken = default)
    {
        var departments = await _departmentRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            DepartmentName = d.DepartmentName,
            DepartmentCode = d.DepartmentCode,
            CreatedOn = d.CreatedOn,
            CreatedBy = d.CreatedBy,
            UpdatedOn = d.UpdatedOn,
            UpdatedBy = d.UpdatedBy,
            IsActive = d.IsActive
        }).ToList();
    }

    public async Task<DepartmentDetailDto?> Handle(GetDepartmentByIdQuery query, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetByIdAsync(query.Id, cancellationToken);

        if (department == null)
            return null;

        return new DepartmentDetailDto
        {
            Id = department.Id,
            DepartmentName = department.DepartmentName,
            DepartmentCode = department.DepartmentCode,
            CreatedOn = department.CreatedOn,
            CreatedBy = department.CreatedBy,
            UpdatedOn = department.UpdatedOn,
            UpdatedBy = department.UpdatedBy,
            IsActive = department.IsActive
        };
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentsByLocationQuery query, CancellationToken cancellationToken = default)
    {
        var location = await _locationRepository.FindByIdAsync(query.LocationId);
        if (location == null)
            return new List<DepartmentDto>();

        var allDepartments = await _departmentRepository.GetAllAsync(query.IncludeInactive, cancellationToken);
        var locationDepartmentMappings = await _locationDepartmentRepository.GetAllAsync(query.IncludeInactive, cancellationToken: cancellationToken);

        var headOfficeLocationIds = locationDepartmentMappings
            .Where(m => string.Equals(m.Location?.LocationName, "Head Office", StringComparison.OrdinalIgnoreCase))
            .Select(m => m.LocationId)
            .Distinct()
            .ToHashSet();

        var allLocationIds = locationDepartmentMappings
            .Where(m => string.Equals(m.Location?.LocationName, "All", StringComparison.OrdinalIgnoreCase))
            .Select(m => m.LocationId)
            .Distinct()
            .ToHashSet();

        var isHeadOfficeLocation = string.Equals(location.LocationName, "Head Office", StringComparison.OrdinalIgnoreCase);

        HashSet<string> allowedDepartmentIds;
        if (isHeadOfficeLocation)
        {
            allowedDepartmentIds = locationDepartmentMappings
                .Where(m => m.LocationId == query.LocationId || allLocationIds.Contains(m.LocationId))
                .Select(m => m.DepartmentId)
                .ToHashSet();
        }
        else
        {
            allowedDepartmentIds = locationDepartmentMappings
                .Where(m => !headOfficeLocationIds.Contains(m.LocationId))
                .Select(m => m.DepartmentId)
                .ToHashSet();
        }

        return allDepartments
            .Where(d => allowedDepartmentIds.Contains(d.Id))
            .Select(d => new DepartmentDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
                DepartmentCode = d.DepartmentCode,
                CreatedOn = d.CreatedOn,
                CreatedBy = d.CreatedBy,
                UpdatedOn = d.UpdatedOn,
                UpdatedBy = d.UpdatedBy,
                IsActive = d.IsActive
            }).ToList();
    }
}
