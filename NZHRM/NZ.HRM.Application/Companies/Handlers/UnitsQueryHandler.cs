using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Units.Queries.GetAllUnits;
using NZ.HRM.Application.Units.Queries.GetUnitById;

namespace NZ.HRM.Application.Units.Handlers;

public class UnitsQueryHandler
{
    private readonly IUnitRepository _unitRepository;

    public UnitsQueryHandler(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<List<UnitDto>> Handle(GetAllUnitsQuery query, CancellationToken cancellationToken = default)
    {
        var units = await _unitRepository.GetAllAsync(query.IncludeInactive, cancellationToken);

        return units.Select(c => new UnitDto
        {
            Id = c.Id,
            UnitCode = c.UnitCode,
            UnitName = c.UnitName,
            UnitNameBangla = c.UnitNameBangla,
            CreatedOn = c.CreatedOn,
            CreatedBy = c.CreatedBy,
            UpdatedOn = c.UpdatedOn,
            UpdatedBy = c.UpdatedBy,
            IsActive = c.IsActive,
            IsCompliant = c.IsCompliant
        }).ToList();
    }
    public async Task<UnitDetailDto?> Handle(GetUnitByIdQuery query, CancellationToken cancellationToken = default)
    {
        var unit = await _unitRepository.GetByIdAsync(query.Id, cancellationToken);

        if (unit == null)
            return null;

        return new UnitDetailDto
        {
            Id = unit.Id,
            UnitCode = unit.UnitCode,
            UnitName = unit.UnitName,
            CreatedOn = unit.CreatedOn,
            CreatedBy = unit.CreatedBy,
            UpdatedOn = unit.UpdatedOn,
            UpdatedBy = unit.UpdatedBy,
            IsActive = unit.IsActive,
            IsCompliant = unit.IsCompliant
        };
    }
}