using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.SectionCells.Commands.CreateSectionCell;
using NZ.HRM.Application.SectionCells.Commands.DeleteSectionCell;
using NZ.HRM.Application.SectionCells.Commands.UpdateSectionCell;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.SectionCells.Handlers;

public class SectionCellCommandHandler
{
    private readonly ISectionCellRepository _sectionCellRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ICellRepository _cellRepository;

    public SectionCellCommandHandler(
        ISectionCellRepository sectionCellRepository,
        ISectionRepository sectionRepository,
        ICellRepository cellRepository)
    {
        _sectionCellRepository = sectionCellRepository;
        _sectionRepository = sectionRepository;
        _cellRepository = cellRepository;
    }

    public async Task<string> Handle(CreateSectionCellCommand command, CancellationToken cancellationToken = default)
    {
        var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        if (!sectionExists)
            throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        var cellExists = await _cellRepository.ExistsAsync(command.CellId, cancellationToken);
        if (!cellExists)
            throw new KeyNotFoundException($"Cell with ID {command.CellId} not found");

        //var sectionCell = new SectionCell
        //{
        //    SectionId = command.SectionId,
        //    CellId = command.CellId,
        //    IsActive = true
        //};

        //return await _sectionCellRepository.AddAsync(sectionCell, cancellationToken);
        return null;
    }

    public async Task Handle(UpdateSectionCellCommand command, CancellationToken cancellationToken = default)
    {
        //var sectionCell = await _sectionCellRepository.GetByIdAsync(command.Id, cancellationToken);
        //if (sectionCell == null)
        //    throw new KeyNotFoundException($"SectionCell with ID {command.Id} not found");

        //var sectionExists = await _sectionRepository.ExistsAsync(command.SectionId, cancellationToken);
        //if (!sectionExists)
        //    throw new KeyNotFoundException($"Section with ID {command.SectionId} not found");

        //var cellExists = await _cellRepository.ExistsAsync(command.CellId, cancellationToken);
        //if (!cellExists)
        //    throw new KeyNotFoundException($"Cell with ID {command.CellId} not found");

        //sectionCell.SectionId = command.SectionId;
        //sectionCell.CellId = command.CellId;

        //await _sectionCellRepository.UpdateAsync(sectionCell, cancellationToken);
    }

    public async Task Handle(DeleteSectionCellCommand command, CancellationToken cancellationToken = default)
    {
        //var sectionCell = await _sectionCellRepository.GetByIdAsync(command.Id, cancellationToken);
        //if (sectionCell == null)
        //    throw new KeyNotFoundException($"SectionCell with ID {command.Id} not found");

        //sectionCell.IsActive = false;
        //await _sectionCellRepository.UpdateAsync(sectionCell, cancellationToken);
    }
}
