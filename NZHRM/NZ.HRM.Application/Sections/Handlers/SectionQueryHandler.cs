using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Sections.Queries.GetAllSections;
using NZ.HRM.Application.Sections.Queries.GetSectionById;

namespace NZ.HRM.Application.Sections.Handlers;

public class SectionQueryHandler
{
    private readonly ISectionRepository _sectionRepository;
    private readonly IDepartmentSectionRepository _departmentSectionRepository;

    public SectionQueryHandler(
        ISectionRepository sectionRepository,
        IDepartmentSectionRepository departmentSectionRepository)
    {
        _sectionRepository = sectionRepository;
        _departmentSectionRepository = departmentSectionRepository;
    }

    public async Task<List<SectionDto>> Handle(GetAllSectionsQuery query, CancellationToken cancellationToken = default)
    {
        var sections = await _sectionRepository.GetAllAsync(query.IncludeInactive, cancellationToken);
        //List<Domain.Entities.Section> sections = new List<Domain.Entities.Section>();
        var overAll = "Overall";
        if (!string.IsNullOrWhiteSpace(query.DepartmentId))
        {
            var filteredSections = await _sectionRepository.GetByDepartmentIdAsync(query.DepartmentId, query.IncludeInactive, cancellationToken);
            if(filteredSections.Any(x => x.SectionName.Equals(overAll, StringComparison.OrdinalIgnoreCase)))
            {
                sections = filteredSections;
            }
        }

        var result = new List<SectionDto>(sections.Count);
        foreach (var section in sections)
        {
            var departmentId = await _departmentSectionRepository.GetDepartmentIdBySectionIdAsync(section.Id, cancellationToken) ?? string.Empty;
            var departmentName = await _departmentSectionRepository.GetDepartmentNameBySectionIdAsync(section.Id, cancellationToken) ?? string.Empty;

            result.Add(new SectionDto
            {
                Id = section.Id,
                DepartmentId = departmentId,
                DepartmentName = departmentName,
                SectionName = section.SectionName,
                CreatedOn = section.CreatedOn,
                CreatedBy = section.CreatedBy,
                UpdatedOn = section.UpdatedOn,
                UpdatedBy = section.UpdatedBy,
                IsActive = section.IsActive
            });
        }

        return result;
    }

    public async Task<SectionDetailDto?> Handle(GetSectionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var section = await _sectionRepository.GetByIdAsync(query.Id, cancellationToken);

        if (section == null)
            return null;

        var mappedDepartmentId = await _departmentSectionRepository.GetDepartmentIdBySectionIdAsync(section.Id, cancellationToken) ?? string.Empty;
        var mappedDepartmentName = await _departmentSectionRepository.GetDepartmentNameBySectionIdAsync(section.Id, cancellationToken) ?? string.Empty;

        return new SectionDetailDto
        {
            Id = section.Id,
            DepartmentId = mappedDepartmentId,
            DepartmentName = mappedDepartmentName,
            SectionName = section.SectionName,
            CreatedOn = section.CreatedOn,
            CreatedBy = section.CreatedBy,
            UpdatedOn = section.UpdatedOn,
            UpdatedBy = section.UpdatedBy,
            IsActive = section.IsActive
        };
    }
}
