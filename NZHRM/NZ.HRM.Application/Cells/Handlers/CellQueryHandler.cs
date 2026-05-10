using NZ.HRM.Application.Cells.Queries.GetAllCells;
using NZ.HRM.Application.Cells.Queries.GetCellById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Cells.Handlers;

public class CellQueryHandler
{
    private readonly ICellRepository _cellRepository;

    public CellQueryHandler(ICellRepository cellRepository)
    {
        _cellRepository = cellRepository;
    }

    public async Task<List<CellDto>> Handle(GetAllCellsQuery query, CancellationToken cancellationToken = default)
    {
        List<NZ.HRM.Domain.Entities.Cell> cells;

        if (!string.IsNullOrEmpty(query.SectionId))
        {
            cells = await _cellRepository.GetBySectionIdAsync(query.SectionId!, cancellationToken);
        }
        else
        {
            cells = await _cellRepository.GetAllAsync(query.IncludeInactive, cancellationToken);
        }

        return cells.Select(c => new CellDto
        {
            Id = c.Id,
            NameEnglish = c.NameEnglish,
            NameBangla = c.NameBangla,
            SectionId = c.SectionId,
            SectionName = c.Section?.SectionName ?? string.Empty,
            CreatedOn = c.CreatedOn,
            CreatedBy = c.CreatedBy,
            UpdatedOn = c.UpdatedOn,
            UpdatedBy = c.UpdatedBy,
            IsActive = c.IsActive
        }).ToList();
    }

    public async Task<CellDetailDto?> Handle(GetCellByIdQuery query, CancellationToken cancellationToken = default)
    {
        var cell = await _cellRepository.GetByIdAsync(query.Id, cancellationToken);
        if (cell == null)
            return null;

        return new CellDetailDto
        {
            Id = cell.Id,
            NameEnglish = cell.NameEnglish,
            NameBangla = cell.NameBangla,
            SectionId = cell.SectionId,
            SectionName = cell.Section?.SectionName ?? string.Empty,
            CreatedOn = cell.CreatedOn,
            CreatedBy = cell.CreatedBy,
            UpdatedOn = cell.UpdatedOn,
            UpdatedBy = cell.UpdatedBy,
            IsActive = cell.IsActive
        };
    }
}
