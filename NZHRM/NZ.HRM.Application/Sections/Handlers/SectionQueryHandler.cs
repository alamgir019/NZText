using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Sections.Queries.GetAllSections;
using NZ.HRM.Application.Sections.Queries.GetSectionById;

namespace NZ.HRM.Application.Sections.Handlers;

public class SectionQueryHandler
{
    private readonly ISectionRepository _sectionRepository;

    public SectionQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<List<SectionDto>> Handle(GetAllSectionsQuery query, CancellationToken cancellationToken = default)
    {
        List<NZ.HRM.Domain.Entities.Section> sections;

        if (!string.IsNullOrEmpty(query.DepartmentId))
        {
            sections = await _sectionRepository.GetByDepartmentIdAsync(query.DepartmentId, cancellationToken);
        }
        else
        {
            sections = await _sectionRepository.GetAllAsync(query.IncludeInactive, cancellationToken);
        }

        return sections.Select(s => new SectionDto
        {
            Id = s.Id,
            DepartmentId = s.DepartmentId,
            DepartmentName = s.Department?.DepartmentName ?? string.Empty,
            SectionName = s.SectionName,
            CreatedOn = s.CreatedOn,
            CreatedBy = s.CreatedBy,
            UpdatedOn = s.UpdatedOn,
            UpdatedBy = s.UpdatedBy,
            IsActive = s.IsActive
        }).ToList();
    }

    public async Task<SectionDetailDto?> Handle(GetSectionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var section = await _sectionRepository.GetByIdAsync(query.Id, cancellationToken);

        if (section == null)
            return null;

        return new SectionDetailDto
        {
            Id = section.Id,
            DepartmentId = section.DepartmentId,
            DepartmentName = section.Department?.DepartmentName ?? string.Empty,
            SectionName = section.SectionName,
            CreatedOn = section.CreatedOn,
            CreatedBy = section.CreatedBy,
            UpdatedOn = section.UpdatedOn,
            UpdatedBy = section.UpdatedBy,
            IsActive = section.IsActive
        };
    }
}
