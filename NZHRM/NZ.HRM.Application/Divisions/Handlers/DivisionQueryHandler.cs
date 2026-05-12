using NZ.HRM.Application.Divisions.Queries;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Divisions.Handlers;

public class DivisionQueryHandler
{
    private readonly IDivisionRepository _divisionRepository;

    public DivisionQueryHandler(IDivisionRepository divisionRepository)
    {
        _divisionRepository = divisionRepository;
    }

    public async Task<List<DivisionDto>> Handle(GetAllDivisionsQuery query, CancellationToken cancellationToken = default)
    {
        var divisions = await _divisionRepository.GetAllAsync(cancellationToken);

        return divisions.Select(d => new DivisionDto
        {
            Id = d.Id,
            DivisionName = d.DivisionName
        }).ToList();
    }
}
