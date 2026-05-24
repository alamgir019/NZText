using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Shifts.Commands.CreateShift;
using NZ.HRM.Application.Shifts.Commands.DeleteShift;
using NZ.HRM.Application.Shifts.Commands.UpdateShift;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Shifts.Handlers;

public class ShiftCommandHandler
{
    private readonly IShiftRepository _shiftRepository;

    public ShiftCommandHandler(IShiftRepository shiftRepository)
    {
        _shiftRepository = shiftRepository;
    }

    public async Task<string> Handle(CreateShiftCommand command, CancellationToken cancellationToken = default)
    {
        var shift = new Shift
        {
            ShiftName = command.ShiftName,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
            SortOrder = command.SortOrder,
            IsActive = true
        };

        return await _shiftRepository.AddAsync(shift, cancellationToken);
    }

    public async Task Handle(UpdateShiftCommand command, CancellationToken cancellationToken = default)
    {
        var shift = await _shiftRepository.GetByIdAsync(command.Id, cancellationToken);
        if (shift == null)
            throw new KeyNotFoundException($"Shift with ID {command.Id} not found");

        shift.ShiftName = command.ShiftName;
        shift.StartTime = command.StartTime;
        shift.EndTime = command.EndTime;
        shift.SortOrder = command.SortOrder;

        await _shiftRepository.UpdateAsync(shift, cancellationToken);
    }

    public async Task Handle(DeleteShiftCommand command, CancellationToken cancellationToken = default)
    {
        var shift = await _shiftRepository.GetByIdAsync(command.Id, cancellationToken);
        if (shift == null)
            throw new KeyNotFoundException($"Shift with ID {command.Id} not found");

        shift.IsActive = false;
        await _shiftRepository.UpdateAsync(shift, cancellationToken);
    }
}
