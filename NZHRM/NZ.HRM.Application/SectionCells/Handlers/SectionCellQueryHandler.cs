using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.SectionCells.Queries.GetAllSectionCells;
using NZ.HRM.Application.SectionCells.Queries.GetSectionCellById;

namespace NZ.HRM.Application.SectionCells.Handlers;

public class SectionCellQueryHandler
{
    private readonly ISectionCellRepository _sectionCellRepository;

    public SectionCellQueryHandler(ISectionCellRepository sectionCellRepository)
    {
        _sectionCellRepository = sectionCellRepository;
    }

    public async Task<List<SectionCellDto>> Handle(GetAllSectionCellsQuery query, CancellationToken cancellationToken = default)
    {
        var mappings = await _sectionCellRepository.GetAllAsync(
            query.IncludeInactive,
            query.SectionId,
            query.CellId,
            cancellationToken);

        return mappings.Select(m => new SectionCellDto
        {
            Id = m.Id,
            SectionId = m.SectionId,
            SectionName = m.Section?.SectionName ?? string.Empty,
            CellId = m.CellId,
            CellName = m.Cell?.NameEnglish ?? string.Empty,
            CreatedOn = m.CreatedOn,
            CreatedBy = m.CreatedBy,
            UpdatedOn = m.UpdatedOn,
            UpdatedBy = m.UpdatedBy,
            IsActive = m.IsActive
        }).ToList();
    }

    public async Task<SectionCellDetailDto?> Handle(GetSectionCellByIdQuery query, CancellationToken cancellationToken = default)
    {
        var mapping = await _sectionCellRepository.GetByIdAsync(query.Id, cancellationToken);
        if (mapping == null)
            return null;

        return new SectionCellDetailDto
        {
            Id = mapping.Id,
            SectionId = mapping.SectionId,
            SectionName = mapping.Section?.SectionName ?? string.Empty,
            CellId = mapping.CellId,
            CellName = mapping.Cell?.NameEnglish ?? string.Empty,
            CreatedOn = mapping.CreatedOn,
            CreatedBy = mapping.CreatedBy,
            UpdatedOn = mapping.UpdatedOn,
            UpdatedBy = mapping.UpdatedBy,
            IsActive = mapping.IsActive
        };
    }
}
