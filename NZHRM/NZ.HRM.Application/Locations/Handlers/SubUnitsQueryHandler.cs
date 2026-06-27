using NZ.HRM.Application.Interface;
using NZ.HRM.Application.SubUnits.Queries;
using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.SubUnits.Handlers
{
    public class SubUnitsQueryHandler
    {
        private readonly ISubUnitRepository _repo;
        public SubUnitsQueryHandler(ISubUnitRepository repo) => _repo = repo;

        public async Task<MstSubunit?> Handle(GetSubUnitByIdQuery query)
            => await _repo.FindByIdAsync(query.Id);

        public async Task<List<MstSubunit>> Handle(GetAllSubUnitsQuery query)
            => await _repo.GetAllAsync();

        public async Task<List<MstSubunit>> Handle(GetSubUnitsByUnitIdQuery query)
            => await _repo.GetByUnitIdAsync(query.UnitId);

        public async Task<List<MstSubunit>> Handle(GetSubUnitsByEmployeeIdQuery query)
            => await _repo.GetByEmployeeIdAsync(query.EmployeeId);
    }
}