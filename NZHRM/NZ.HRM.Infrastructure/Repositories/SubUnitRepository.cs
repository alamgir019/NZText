using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interface;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories
{
    public class SubUnitRepository : ISubUnitRepository
    {
        private readonly ApplicationDbContext _db;
        public SubUnitRepository(ApplicationDbContext db) => _db = db;

        public async Task<MstSubunit?> FindByIdAsync(string id) => await _db.MstSubunits.FindAsync(id);

        public async Task AddAsync(MstSubunit subUnit) => await _db.MstSubunits.AddAsync(subUnit);

        public async Task RemoveAsync(MstSubunit subUnit)
        {
            _db.MstSubunits.Remove(subUnit);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(MstSubunit subUnit)
        {
            _db.MstSubunits.Update(subUnit);
            await Task.CompletedTask;
        }

        public async Task<List<MstSubunit>> GetAllAsync() => await _db.MstSubunits.ToListAsync();

        public async Task<List<MstSubunit>> GetByCompanyIdAsync(string companyId)
        {
            return await _db.MstSubunits
                .Where(cl => cl.UnitId == companyId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<MstSubunit>> GetByEmployeeIdAsync(string employeeId)
        {
            var companyId = await _db.HrmEmployeeMasters
                .Where(e => e.Id == employeeId && e.IsActive)
                .Select(e => e.Employment!.UnitId)
                .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(companyId))
            {
                return new List<MstSubunit>();
            }

            return await _db.MstSubunits
                .Where(cl => cl.UnitId == companyId && cl.IsActive)
                .Distinct()
                .ToListAsync();
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}