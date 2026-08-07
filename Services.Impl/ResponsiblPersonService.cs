using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services.Impl
{
    public class ResponsiblPersonService : IResponsiblPersonService
    {
        private readonly AppDbContext _context;

        public ResponsiblPersonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResponsiblPerson>> GetAllByKontragentAsync(int kontragentId)
        {
            return await _context.ResponsiblPersons
                .Where(p => p.PersonKontragent != null && p.PersonKontragent.ID == kontragentId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ResponsiblPerson person)
        {
            _context.ResponsiblPersons.Add(person);
            await _context.SaveChangesAsync();
        }
    }
}
