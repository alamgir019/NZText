using NZ.HRM.Application.Interface;
using NZ.HRM.Application.Locations.Commands;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Domain.Helper;

namespace NZ.HRM.Application.Locations.Handlers
{
    public class LocationCommandHandler
    {
        private readonly ILocationRepository _repo;
        public LocationCommandHandler(ILocationRepository repo) => _repo = repo;

        public async Task<string> Handle(CreateLocationCommand cmd)
        {
            var location = new Location
            {
                Id = IdentityGenerator.Next(),
                LocationName = cmd.LocationName,
                DistrictId = cmd.DistrictId,
                CreatedBy = "system",
                UpdatedBy = "system",
                IsActive = true
            };
            await _repo.AddAsync(location);
            await _repo.SaveChangesAsync();
            return location.Id;
        }

        public async Task Handle(UpdateLocationCommand cmd)
        {
            var location = await _repo.FindByIdAsync(cmd.Id);
            if (location is null) throw new Exception("Location not found");
            
            location.LocationName = cmd.LocationName;
            location.DistrictId = cmd.DistrictId;
            location.UpdatedOn = DateTime.UtcNow;
            location.UpdatedBy = cmd.UpdatedBy;
            
            await _repo.UpdateAsync(location);
            await _repo.SaveChangesAsync();
        }

        public async Task Handle(DeleteLocationCommand cmd)
        {
            var location = await _repo.FindByIdAsync(cmd.Id);
            if (location is null) throw new Exception("Location not found");
            
            await _repo.RemoveAsync(location);
            await _repo.SaveChangesAsync();
        }
    }
}