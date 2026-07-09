using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    private readonly ApplicationDbContext _context;

    public BankRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LookBanking>> GetAllAsync(bool includeInactive = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Banks.AsQueryable();
        if (!includeInactive)
            query = query.Where(b => b.IsActive);

        return await query.OrderBy(b => b.BankingName).ToListAsync(cancellationToken);
    }

    public async Task<LookBanking?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Banks.FirstOrDefaultAsync(b => b.Id == id && b.IsActive, cancellationToken);
    }

    public async Task<string> AddAsync(LookBanking bank, CancellationToken cancellationToken = default)
    {
        _context.Banks.Add(bank);
        await _context.SaveChangesAsync(cancellationToken);
        return bank.Id;
    }

    public async Task UpdateAsync(LookBanking bank, CancellationToken cancellationToken = default)
    {
        _context.Banks.Update(bank);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(LookBanking bank, CancellationToken cancellationToken = default)
    {
        _context.Banks.Remove(bank);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Banks.AnyAsync(b => b.Id == id, cancellationToken);
    }
}
