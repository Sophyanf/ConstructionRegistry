using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services.Impl
{
    public class KontragentService : IKontragentService
    {
        private readonly AppDbContext _context;

        public KontragentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Kontragent>> GetAllAsync()
        {
            return await _context.Kontragents.AsNoTracking().ToListAsync();
        }

        public async Task<Kontragent?> GetByInnAsync(string inn)
        {
            return await _context.Kontragents
                .FirstOrDefaultAsync(k => k.KontragentINN == inn);
        }

        public async Task AddAsync(Kontragent kontragent)
        {
            _context.Kontragents.Add(kontragent);
            await _context.SaveChangesAsync();
        }
    }
}
