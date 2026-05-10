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

    public CellCommandHandler(ICellRepository cellRepository, ISectionRepository sectionRepository)
    {
        _cellRepository = cellRepository;
        _sectionRepository = sectionRepository;
    }

    public async Task<string> Handle(CreateCellCommand command, CancellationToken cancellationToken = default)
    {
        // validate section
        var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        var cell = new Cell
        {
            NameEnglish = command.NameEnglish,
            NameBangla = command.NameBangla,
            SectionId = command.SectionId,
            IsActive = true
        };

        return await _cellRepository.AddAsync(cell, cancellationToken);
    }

    public async Task Handle(UpdateCellCommand command, CancellationToken cancellationToken = default)
    {
        var cell = await _cellRepository.GetByIdAsync(command.Id, cancellationToken);
        if (cell == null)
            throw new KeyNotFoundException($"Cell with ID {command.Id} not found");

        var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        cell.NameEnglish = command.NameEnglish;
        cell.NameBangla = command.NameBangla;
        cell.SectionId = command.SectionId;

        await _cellRepository.UpdateAsync(cell, cancellationToken);
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
