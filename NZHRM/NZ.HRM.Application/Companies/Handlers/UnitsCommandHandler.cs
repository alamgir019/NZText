using NZ.HRM.Application.Companies.Commands.CreateUnit;
using NZ.HRM.Application.Companies.Commands.DeleteUnit;
using NZ.HRM.Application.Companies.Commands.UpdateUnit;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Companies.Handlers;

public class UnitsCommandHandler
{
    private readonly IUnitRepository _unitRepository;

    public UnitsCommandHandler(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task Handle(DeleteUnitCommand command, CancellationToken cancellationToken = default)
    {
        var unit = await _unitRepository.GetByIdAsync(command.Id, cancellationToken);

        if (unit == null)
            throw new KeyNotFoundException($"Unit with ID {command.Id} not found");

        // Soft delete
        unit.IsActive = false;
        await _unitRepository.UpdateAsync(unit, cancellationToken);

        // Or hard delete
        // await _unitRepository.DeleteAsync(unit, cancellationToken);
    }

    public async Task Handle(UpdateUnitCommand command, CancellationToken cancellationToken = default)
    {
        var unit = await _unitRepository.GetByIdAsync(command.Id, cancellationToken);

        if (unit == null)
            throw new KeyNotFoundException($"Unit with ID {command.Id} not found");

        unit.UnitCode = command.UnitCode;
        unit.UnitName = command.UnitName;
        unit.IsCompliant = command.IsCompliant;

        await _unitRepository.UpdateAsync(unit, cancellationToken);
    }

    public async Task<string> Handle(CreateUnitCommand command, CancellationToken cancellationToken = default)
    {
        var unit = new MstUnit
        {
            UnitCode = command.UnitCode,
            UnitName = command.UnitName,
            IsCompliant = command.IsCompliant,
            IsActive = true
        };

        return await _unitRepository.AddAsync(unit, cancellationToken);
    }
}