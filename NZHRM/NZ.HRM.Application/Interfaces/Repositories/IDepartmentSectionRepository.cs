namespace NZ.HRM.Application.Interfaces.Repositories;

public interface IDepartmentSectionRepository
{
    Task<string?> GetDepartmentIdBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default);
    Task<string?> GetDepartmentNameBySectionIdAsync(string sectionId, CancellationToken cancellationToken = default);
    Task SetDepartmentForSectionAsync(string sectionId, string departmentId, CancellationToken cancellationToken = default);
}
