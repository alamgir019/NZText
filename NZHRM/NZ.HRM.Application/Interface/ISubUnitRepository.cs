using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interface
{
    public interface ISubUnitRepository
    {
        Task<MstSubunit?> FindByIdAsync(string id);
        Task<List<MstSubunit>> GetAllAsync();
        Task<List<MstSubunit>> GetByCompanyIdAsync(string companyId);
        Task<List<MstSubunit>> GetByEmployeeIdAsync(string employeeId);
        Task AddAsync(MstSubunit subUnit);
        Task RemoveAsync(MstSubunit subUnit);
        Task UpdateAsync(MstSubunit subUnit);
        Task SaveChangesAsync();
    }
}