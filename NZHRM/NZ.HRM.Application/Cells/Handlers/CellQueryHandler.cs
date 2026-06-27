using NZ.HRM.Application.Cells.Queries.GetAllCells;
using NZ.HRM.Application.Cells.Queries.GetCellById;
using NZ.HRM.Application.Cells.Queries.GetCellsBySectionId;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Cells.Handlers;

public class CellQueryHandler
{
    private readonly ICellRepository _cellRepository;

    public CellQueryHandler(
        ICellRepository cellRepository)
    {
        _cellRepository = cellRepository;
    }

    public async Task<List<CellDto>> Handle(GetAllCellsQuery query, CancellationToken cancellationToken = default)
    {
        List<Domain.Entities.MstCell> cells;

        if (!string.IsNullOrEmpty(query.SectionId))
        {
            cells = await _cellRepository.GetBySectionIdAsync(query.SectionId!, query.IncludeInactive, cancellationToken);
        }
        else
        {
            cells = await _cellRepository.GetAllAsync(query.IncludeInactive, cancellationToken);
        }

        var result = new List<CellDto>(cells.Count);
        foreach (var cell in cells)
        {
            var sectionId = cell.Section?.Id ?? string.Empty;
            var sectionName = cell.Section?.SectionName ?? string.Empty;

            result.Add(new CellDto
            {
                Id = cell.Id,
                NameEnglish = cell.NameEnglish,
                NameBangla = cell.NameBangla,
                SectionId = sectionId,
                SectionName = sectionName,
                CreatedOn = cell.CreatedOn,
                CreatedBy = cell.CreatedBy,
                UpdatedOn = cell.UpdatedOn,
                UpdatedBy = cell.UpdatedBy,
                IsActive = cell.IsActive
            });
        }

        return result;
    }
    
    public async Task<CellDetailDto?> Handle(GetCellByIdQuery query, CancellationToken cancellationToken = default)
    {
        var cell = await _cellRepository.GetByIdAsync(query.Id, cancellationToken);
        if (cell == null)
            return null;

        //var mappedSectionId = await _sectionCellRepository.GetSectionIdByCellIdAsync(cell.Id, cancellationToken) ?? string.Empty;
        //var mappedSectionName = await _sectionCellRepository.GetSectionNameByCellIdAsync(cell.Id, cancellationToken) ?? string.Empty;

        //return new CellDetailDto
        //{
        //    Id = cell.Id,
        //    NameEnglish = cell.NameEnglish,
        //    NameBangla = cell.NameBangla,
        //    SectionId = mappedSectionId,
        //    SectionName = mappedSectionName,
        //    CreatedOn = cell.CreatedOn,
        //    CreatedBy = cell.CreatedBy,
        //    UpdatedOn = cell.UpdatedOn,
        //    UpdatedBy = cell.UpdatedBy,
        //    IsActive = cell.IsActive
        //};
        return null;
    }
}
