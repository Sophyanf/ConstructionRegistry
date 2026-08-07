using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services.Impl
{
    public class ConstructionObjectService : IConstructionObjectService
    {
        private readonly AppDbContext _context;

        public ConstructionObjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConstructionObject>> GetAllWithCustomerAsync()
        {
            return await _context.ConstructionObjects
                .Include(o => o.Customer)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ConstructionObject obj)
        {
            _context.ConstructionObjects.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.ConstructionObjects.FindAsync(id);
            if (obj != null)
            {
                _context.ConstructionObjects.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }
    }
}
