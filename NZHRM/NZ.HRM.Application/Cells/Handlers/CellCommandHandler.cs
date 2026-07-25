using NZ.HRM.Application.Cells.Commands.CreateCell;
using NZ.HRM.Application.Cells.Commands.DeleteCell;
using NZ.HRM.Application.Cells.Commands.UpdateCell;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Cells.Handlers;

public class CellCommandHandler
{
    private readonly ICellRepository _cellRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ISectionCellRepository _sectionCellRepository;

    public CellCommandHandler(
        ICellRepository cellRepository,
        ISectionRepository sectionRepository,
        ISectionCellRepository sectionCellRepository)
    {
        _cellRepository = cellRepository;
        _sectionRepository = sectionRepository;
        _sectionCellRepository = sectionCellRepository;
    }

    public async Task<string> Handle(CreateCellCommand command, CancellationToken cancellationToken = default)
    {
        // validate section
        var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        var cell = new MstCell
        {
            CellName = command.CellName,
            NameBangla = command.CellNameBangla,
            IsActive = true
        };

        var cellId = await _cellRepository.AddAsync(cell, cancellationToken);

        //await _sectionCellRepository.SetSectionForCellAsync(
        //    cellId,
        //    command.SectionId,
        //    cancellationToken);

        return cellId;
    }

    public async Task Handle(UpdateCellCommand command, CancellationToken cancellationToken = default)
    {
        var cell = await _cellRepository.GetByIdAsync(command.Id, cancellationToken);
        if (cell == null)
            throw new KeyNotFoundException($"Cell with ID {command.Id} not found");

        var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        cell.CellName = command.CellName;
        cell.NameBangla = command.CellNameBangla;

        await _cellRepository.UpdateAsync(cell, cancellationToken);

        //await _sectionCellRepository.SetSectionForCellAsync(
        //    cell.Id,
        //    command.SectionId,
        //    cancellationToken);
    }

    public async Task Handle(DeleteCellCommand command, CancellationToken cancellationToken = default)
    {
        var cell = await _cellRepository.GetByIdAsync(command.Id, cancellationToken);
        if (cell == null)
            throw new KeyNotFoundException($"Cell with ID {command.Id} not found");

        cell.IsActive = false;
        await _cellRepository.UpdateAsync(cell, cancellationToken);
    }
}
