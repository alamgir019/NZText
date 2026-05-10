using NZ.HRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZ.HRM.Application.Interface
{
    public interface ILocationRepository
    {
        Task<Location?> FindByIdAsync(string id);
        Task<List<Location>> GetAllAsync();
        Task AddAsync(Location location);
        Task RemoveAsync(Location location);
        Task UpdateAsync(Location location);
        Task SaveChangesAsync();
    }
}