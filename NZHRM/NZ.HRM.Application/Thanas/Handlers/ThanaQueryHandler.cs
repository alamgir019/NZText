using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Thanas.Queries;

namespace NZ.HRM.Application.Thanas.Handlers;

public class ThanaQueryHandler
{
    private readonly IThanaRepository _thanaRepository;

    public ThanaQueryHandler(IThanaRepository thanaRepository)
    {
        _thanaRepository = thanaRepository;
    }

    public async Task<List<ThanaDto>> Handle(GetThanasByDistrictIdQuery query, CancellationToken cancellationToken = default)
    {
        var thanas = await _thanaRepository.GetByDistrictIdAsync(query.DistrictId, cancellationToken);

        return thanas.Select(t => new ThanaDto
        {
            Id = t.Id,
            ThanaName = t.ThanaName,
            DistrictId = t.DistrictId,
            DistrictName = t.District?.DistrictName ?? string.Empty
        }).ToList();
    }
}
