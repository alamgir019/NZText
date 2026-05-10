using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Entities;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuRepository(ApplicationDbContext db) => _db = db;

        public async Task<Menu?> FindByIdAsync(string id) => await _db.Menus.FindAsync(id);

        public async Task AddAsync(Menu menu) => await _db.Menus.AddAsync(menu);

        public async Task RemoveAsync(Menu menu)
        {
            _db.Menus.Remove(menu);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Menu menu)
        {
            _db.Menus.Update(menu);
            await Task.CompletedTask;
        }

        public async Task<List<Menu>> GetAllAsync() => await _db.Menus.ToListAsync();

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}