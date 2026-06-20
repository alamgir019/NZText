using NZ.HRM.Application.Interface;
using NZ.HRM.Application.Locations.Queries;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Locations.Handlers
{
    public class LocationQueryHandler
    {
        private readonly ILocationRepository _repo;
        public LocationQueryHandler(ILocationRepository repo) => _repo = repo;

        public async Task<MstSubunit?> Handle(GetLocationByIdQuery query)
            => await _repo.FindByIdAsync(query.Id);

        public async Task<List<MstSubunit>> Handle(GetAllLocationsQuery query)
            => await _repo.GetAllAsync();

        public async Task<List<MstSubunit>> Handle(GetLocationsByCompanyIdQuery query)
            => await _repo.GetByCompanyIdAsync(query.CompanyId);

        public async Task<List<MstSubunit>> Handle(GetLocationsByEmployeeIdQuery query)
            => await _repo.GetByEmployeeIdAsync(query.EmployeeId);
    }
}