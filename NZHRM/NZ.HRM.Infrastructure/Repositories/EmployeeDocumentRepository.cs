using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EmployeeDocumentRepository : IEmployeeDocumentRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeDocumentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<HrmEmployeeDocument>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeDocuments
            .Include(ed => ed.Employee)
            .OrderBy(ed => ed.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<HrmEmployeeDocument>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeDocuments
            .Include(ed => ed.Employee)
            .Where(ed => ed.EmployeeId == employeeId)
            .OrderBy(ed => ed.SortOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<HrmEmployeeDocument?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeDocuments
            .Include(ed => ed.Employee)
            .FirstOrDefaultAsync(ed => ed.Id == id, cancellationToken);
    }

    public async Task<string> AddAsync(HrmEmployeeDocument employeeDocument, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeDocuments.Add(employeeDocument);
        await _context.SaveChangesAsync(cancellationToken);
        return employeeDocument.Id;
    }

    public async Task<string> AddRangeAsync(List<HrmEmployeeDocument> employeeDocuments, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeDocuments.AddRange(employeeDocuments);
        await _context.SaveChangesAsync(cancellationToken);
        return string.Join(",", employeeDocuments.Select(ed => ed.Id));
    }

    public async Task UpdateAsync(HrmEmployeeDocument employeeDocument, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeDocuments.Update(employeeDocument);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(HrmEmployeeDocument employeeDocument, CancellationToken cancellationToken = default)
    {
        _context.HrmEmployeeDocuments.Remove(employeeDocument);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.HrmEmployeeDocuments
            .AnyAsync(ed => ed.Id == id, cancellationToken);
    }
}