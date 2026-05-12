using NZ.HRM.Application.Districts.Queries;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.Districts.Handlers;

public class DistrictQueryHandler
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictQueryHandler(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }

    public async Task<List<DistrictDto>> Handle(GetDistrictsByDivisionIdQuery query, CancellationToken cancellationToken = default)
    {
        var districts = await _districtRepository.GetByDivisionIdAsync(query.DivisionId, cancellationToken);

        return districts.Select(d => new DistrictDto
        {
            Id = d.Id,
            DistrictName = d.DistrictName,
            DivisionId = d.DivisionId,
            DivisionName = d.Division?.DivisionName ?? string.Empty
        }).ToList();
    }
}
