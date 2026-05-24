using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Shifts.Queries.GetAllShifts;
using NZ.HRM.Application.Shifts.Queries.GetShiftById;

namespace NZ.HRM.Application.Shifts.Handlers;

public class ShiftQueryHandler
{
    private readonly IShiftRepository _shiftRepository;

    public ShiftQueryHandler(IShiftRepository shiftRepository)
    {
        _shiftRepository = shiftRepository;
    }

    public async Task<List<ShiftDto>> Handle(GetAllShiftsQuery query, CancellationToken cancellationToken = default)
    {
        var shifts = await _shiftRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return shifts.Select(s => new ShiftDto
        {
            Id = s.Id,
            ShiftName = s.ShiftName,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            SortOrder = s.SortOrder,
            CreatedOn = s.CreatedOn,
            CreatedBy = s.CreatedBy,
            UpdatedOn = s.UpdatedOn,
            UpdatedBy = s.UpdatedBy,
            IsActive = s.IsActive
        }).ToList();
    }

    public async Task<ShiftDetailDto?> Handle(GetShiftByIdQuery query, CancellationToken cancellationToken = default)
    {
        var shift = await _shiftRepository.GetByIdAsync(query.Id, cancellationToken);
        if (shift == null)
            return null;

        return new ShiftDetailDto
        {
            Id = shift.Id,
            ShiftName = shift.ShiftName,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            SortOrder = shift.SortOrder,
            CreatedOn = shift.CreatedOn,
            CreatedBy = shift.CreatedBy,
            UpdatedOn = shift.UpdatedOn,
            UpdatedBy = shift.UpdatedBy,
            IsActive = shift.IsActive
        };
    }
}
