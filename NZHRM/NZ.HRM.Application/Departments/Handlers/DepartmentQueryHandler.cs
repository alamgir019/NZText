using NZ.HRM.Application.Departments.Queries.GetAllDepartments;
using NZ.HRM.Application.Departments.Queries.GetDepartmentById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Departments.Handlers;

public class DepartmentQueryHandler
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
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
}
