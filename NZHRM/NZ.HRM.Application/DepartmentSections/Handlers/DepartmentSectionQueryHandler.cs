using NZ.HRM.Application.DepartmentSections.Queries.GetAllDepartmentSections;
using NZ.HRM.Application.DepartmentSections.Queries.GetDepartmentSectionById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.DepartmentSections.Handlers;

public class DepartmentSectionQueryHandler
{
    private readonly IDepartmentSectionRepository _departmentSectionRepository;

    public DepartmentSectionQueryHandler(IDepartmentSectionRepository departmentSectionRepository)
    {
        _departmentSectionRepository = departmentSectionRepository;
    }

    //public async Task<List<DepartmentSectionDto>> Handle(GetAllDepartmentSectionsQuery query, CancellationToken cancellationToken = default)
    //{
    //    var mappings = await _departmentSectionRepository.GetAllAsync(
    //        query.IncludeInactive,
    //        query.DepartmentId,
    //        query.SectionId,
    //        cancellationToken);

    //    return mappings.Select(m => new DepartmentSectionDto
    //    {
    //        Id = m.Id,
    //        DepartmentId = m.DepartmentId,
    //        DepartmentName = m.Department?.DepartmentName ?? string.Empty,
    //        SectionId = m.SectionId,
    //        SectionName = m.Section?.SectionName ?? string.Empty,
    //        CreatedOn = m.CreatedOn,
    //        CreatedBy = m.CreatedBy,
    //        UpdatedOn = m.UpdatedOn,
    //        UpdatedBy = m.UpdatedBy,
    //        IsActive = m.IsActive
    //    }).ToList();
    //}

    //public async Task<DepartmentSectionDetailDto?> Handle(GetDepartmentSectionByIdQuery query, CancellationToken cancellationToken = default)
    //{
    //    var mapping = await _departmentSectionRepository.GetByIdAsync(query.Id, cancellationToken);
    //    if (mapping == null)
    //        return null;

    //    return new DepartmentSectionDetailDto
    //    {
    //        Id = mapping.Id,
    //        DepartmentId = mapping.DepartmentId,
    //        DepartmentName = mapping.Department?.DepartmentName ?? string.Empty,
    //        SectionId = mapping.SectionId,
    //        SectionName = mapping.Section?.SectionName ?? string.Empty,
    //        CreatedOn = mapping.CreatedOn,
    //        CreatedBy = mapping.CreatedBy,
    //        UpdatedOn = mapping.UpdatedOn,
    //        UpdatedBy = mapping.UpdatedBy,
    //        IsActive = mapping.IsActive
    //    };
    //}
}
