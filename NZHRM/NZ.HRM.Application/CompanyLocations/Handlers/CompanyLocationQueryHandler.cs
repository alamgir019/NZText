using NZ.HRM.Application.CompanyLocations.Queries.GetAllCompanyLocations;
using NZ.HRM.Application.CompanyLocations.Queries.GetCompanyLocationById;
using NZ.HRM.Application.Interfaces.Repositories;

namespace NZ.HRM.Application.CompanyLocations.Handlers;

public class CompanyLocationQueryHandler
{
    private readonly ICompanyLocationRepository _companyLocationRepository;

    public CompanyLocationQueryHandler(ICompanyLocationRepository companyLocationRepository)
    {
        _companyLocationRepository = companyLocationRepository;
    }

    public async Task<List<CompanyLocationDto>> Handle(GetAllCompanyLocationsQuery query, CancellationToken cancellationToken = default)
    {
        var mappings = await _companyLocationRepository.GetAllAsync(
            query.IncludeInactive,
            query.CompanyId,
            query.LocationId,
            cancellationToken);

        return mappings.Select(m => new CompanyLocationDto
        {
            Id = m.Id,
            CompanyId = m.CompanyId,
            CompanyName = m.Company?.CompanyName ?? string.Empty,
            LocationId = m.LocationId,
            LocationName = m.Location?.LocationName ?? string.Empty,
            CreatedOn = m.CreatedOn,
            CreatedBy = m.CreatedBy,
            UpdatedOn = m.UpdatedOn,
            UpdatedBy = m.UpdatedBy,
            IsActive = m.IsActive
        }).ToList();
    }

    public async Task<CompanyLocationDetailDto?> Handle(GetCompanyLocationByIdQuery query, CancellationToken cancellationToken = default)
    {
        var mapping = await _companyLocationRepository.GetByIdAsync(query.Id, cancellationToken);
        if (mapping == null)
            return null;

        return new CompanyLocationDetailDto
        {
            Id = mapping.Id,
            CompanyId = mapping.CompanyId,
            CompanyName = mapping.Company?.CompanyName ?? string.Empty,
            LocationId = mapping.LocationId,
            LocationName = mapping.Location?.LocationName ?? string.Empty,
            CreatedOn = mapping.CreatedOn,
            CreatedBy = mapping.CreatedBy,
            UpdatedOn = mapping.UpdatedOn,
            UpdatedBy = mapping.UpdatedBy,
            IsActive = mapping.IsActive
        };
    }
}
