namespace NZ.HRM.Application.Interfaces.Repositories;

public interface ISectionCellRepository
{
    Task<string?> GetSectionIdByCellIdAsync(string cellId, CancellationToken cancellationToken = default);
    Task<string?> GetSectionNameByCellIdAsync(string cellId, CancellationToken cancellationToken = default);
    Task SetSectionForCellAsync(string cellId, string sectionId, CancellationToken cancellationToken = default);
}
