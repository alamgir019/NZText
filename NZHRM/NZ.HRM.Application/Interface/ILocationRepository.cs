using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interface
{
    public interface ILocationRepository
    {
        Task<MstSubunit?> FindByIdAsync(string id);
        Task<List<MstSubunit>> GetAllAsync();
        Task<List<MstSubunit>> GetByCompanyIdAsync(string companyId);
        Task<List<MstSubunit>> GetByEmployeeIdAsync(string employeeId);
        Task AddAsync(MstSubunit location);
        Task RemoveAsync(MstSubunit location);
        Task UpdateAsync(MstSubunit location);
        Task SaveChangesAsync();
    }
}