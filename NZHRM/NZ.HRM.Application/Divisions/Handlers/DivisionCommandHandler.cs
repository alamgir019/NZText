using NZ.HRM.Application.Divisions.Commands.CreateDivision;
using NZ.HRM.Application.Divisions.Commands.DeleteDivision;
using NZ.HRM.Application.Divisions.Commands.UpdateDivision;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Divisions.Handlers;

public class DivisionCommandHandler
{
    private readonly IDivisionRepository _divisionRepository;

    public DivisionCommandHandler(IDivisionRepository divisionRepository)
    {
        _divisionRepository = divisionRepository;
    }

    public async Task<string> Handle(CreateDivisionCommand command, CancellationToken cancellationToken = default)
    {
        var division = new LookDivision
        {
            DivisionName = command.DivisionName,
            DivisionNameBangla = command.DivisionNameBangla,
            IsActive = true
        };

        return await _divisionRepository.AddAsync(division, cancellationToken);
    }

    public async Task Handle(UpdateDivisionCommand command, CancellationToken cancellationToken = default)
    {
        var division = await _divisionRepository.GetByIdAsync(command.Id, cancellationToken);
        if (division == null) throw new KeyNotFoundException($"Division with ID {command.Id} not found");

        division.DivisionName = command.DivisionName;
        division.DivisionNameBangla = command.DivisionNameBangla;

        await _divisionRepository.UpdateAsync(division, cancellationToken);
    }

    public async Task Handle(DeleteDivisionCommand command, CancellationToken cancellationToken = default)
    {
        var division = await _divisionRepository.GetByIdAsync(command.Id, cancellationToken);
        if (division == null) throw new KeyNotFoundException($"Division with ID {command.Id} not found");

        division.IsActive = false;
        await _divisionRepository.UpdateAsync(division, cancellationToken);
    }
}
