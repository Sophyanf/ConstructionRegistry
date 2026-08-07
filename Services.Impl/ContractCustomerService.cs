using ConstructionRegistry.Models;
using ConstructionRegistry.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services.Impl
{
    public class ContractCustomerService : IContractCustomerService
    {
        private readonly AppDbContext _context;

        public ContractCustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ContractCustomer contract)
        {
            _context.ContractCustomers.Add(contract);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContractCustomer>> GetByKontragentIdAsync(int kontragentId)
        {
            return await _context.ContractCustomers
                .Where(cc => cc.Kontragent.ID == kontragentId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
