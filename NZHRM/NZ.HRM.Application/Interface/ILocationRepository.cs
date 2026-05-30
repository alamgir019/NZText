using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interface
{
    public interface ILocationRepository
    {
        Task<Location?> FindByIdAsync(string id);
        Task<List<Location>> GetAllAsync();
        Task<List<Location>> GetByCompanyIdAsync(string companyId);
        Task<List<Location>> GetByEmployeeIdAsync(string employeeId);
        Task AddAsync(Location location);
        Task RemoveAsync(Location location);
        Task UpdateAsync(Location location);
        Task SaveChangesAsync();
    }
}