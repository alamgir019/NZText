using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interface;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db) => _db = db;

        public async Task<Location?> FindByIdAsync(string id) => await _db.Locations.FindAsync(id);

        public async Task AddAsync(Location location) => await _db.Locations.AddAsync(location);

        public async Task RemoveAsync(Location location)
        {
            _db.Locations.Remove(location);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Location location)
        {
            _db.Locations.Update(location);
            await Task.CompletedTask;
        }

        public async Task<List<Location>> GetAllAsync() => await _db.Locations.ToListAsync();

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}