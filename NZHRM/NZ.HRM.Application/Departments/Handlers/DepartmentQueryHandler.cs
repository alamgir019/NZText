using NZ.HRM.Application.Departments.Queries.GetAllDepartments;
using NZ.HRM.Application.Departments.Queries.GetDepartmentsByLocation;
using NZ.HRM.Application.Departments.Queries.GetDepartmentById;
using NZ.HRM.Application.Interface;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.ComplexUnitDepartments.Queries.GetAllComplexUnitDepartments;

namespace NZ.HRM.Application.Departments.Handlers;

public class DepartmentQueryHandler
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ISubUnitRepository _locationRepository;
    private readonly IComplexUnitDepartmentRepository _complexUnitDepartmentRepository;

    public DepartmentQueryHandler(
        IDepartmentRepository departmentRepository,
        ISubUnitRepository locationRepository,
        IComplexUnitDepartmentRepository complexUnitDepartmentRepository)
    {
        _departmentRepository = departmentRepository;
        _locationRepository = locationRepository;
        _complexUnitDepartmentRepository = complexUnitDepartmentRepository;
    }

    public async Task<List<ComplexUnitDepartmentDto>> Handle(GetAllDepartmentsQuery query, CancellationToken cancellationToken = default)
    {
        var departments = await _departmentRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return departments.Select(d => new ComplexUnitDepartmentDto
        {
            DepartmentId = d.Id,
            DepartmentName = d.DepartmentName,
            DepartmentCode = d.DepartmentCode,
            //CreatedOn = d.CreatedOn,
            //CreatedBy = d.CreatedBy,
            //UpdatedOn = d.UpdatedOn,
            //UpdatedBy = d.UpdatedBy,
            //IsActive = d.IsActive
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

    public async Task<List<ComplexUnitDepartmentDto>> Handle(GetDepartmentsByComplexUnitQuery query, CancellationToken cancellationToken = default)
    {
        var location = await _locationRepository.FindByIdAsync(query.ComplexId);
        if (location == null)
            return new List<ComplexUnitDepartmentDto>();

        var complexUnitDepartmentMappings = await _complexUnitDepartmentRepository.GetAllAsync(query.IncludeInactive, query.ComplexId, query.UnitId, cancellationToken);

        return complexUnitDepartmentMappings
            .Select(d => new ComplexUnitDepartmentDto
            {
                ComplexId = d.ComplexId,
                ComplexName = d.Complex?.GroupName ?? string.Empty,
                UnitId = d.UnitId,
                UnitName = d.Unit?.UnitName ?? string.Empty,
                DepartmentId = d.Id,
                DepartmentName = d.Department?.DepartmentName ?? string.Empty,
                DepartmentCode = d.Department?.DepartmentCode ?? string.Empty
            }).ToList();
    }
}
