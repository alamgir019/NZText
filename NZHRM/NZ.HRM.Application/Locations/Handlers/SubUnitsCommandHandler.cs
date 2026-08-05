using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Locations.Commands;
using NZ.HRM.Application.SubUnits.Commands;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Helper;

namespace NZ.HRM.Application.SubUnits.Handlers
{
    public class SubUnitsCommandHandler
    {
        private readonly ISubUnitRepository _repo;
        public SubUnitsCommandHandler(ISubUnitRepository repo) => _repo = repo;

        public async Task<string> Handle(CreateSubUnitCommand cmd)
        {
            var subUnit = new MstSubunit
            {
                Id = IdentityGenerator.Next(),
                SubunitName = cmd.SubUnitName,
                UnitId = cmd.UnitId,
                CreatedBy = "system",
                UpdatedBy = "system",
                IsActive = true
            };
            await _repo.AddAsync(subUnit);
            await _repo.SaveChangesAsync();
            return subUnit.Id;
        }

        public async Task Handle(UpdateSubUnitCommand cmd)
        {
            var subUnit = await _repo.FindByIdAsync(cmd.Id);
            if (subUnit is null) throw new Exception("SubUnit not found");

            subUnit.SubunitName = cmd.SubUnitName;
            subUnit.UnitId = cmd.UnitId;
            subUnit.UpdatedOn = DateTime.UtcNow;
            subUnit.UpdatedBy = cmd.UpdatedBy;
            
            await _repo.UpdateAsync(subUnit);
            await _repo.SaveChangesAsync();
        }

        public async Task Handle(DeleteSubUnitCommand cmd)
        {
            var subUnit = await _repo.FindByIdAsync(cmd.Id);
            if (subUnit is null) throw new Exception("SubUnit not found");
            
            await _repo.RemoveAsync(subUnit);
            await _repo.SaveChangesAsync();
        }
    }
}