using NZ.HRM.Domain.Entities;

namespace NZ.HRM.Application.Interfaces.Repositories
{
    public interface IEmployeeNomineeRepository
    {
        Task<string> AddAsync(HrmEmployeeNominee employeeNominee, CancellationToken cancellationToken = default);
    }
}