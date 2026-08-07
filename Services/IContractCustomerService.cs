using ConstructionRegistry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services
{
    public interface IContractCustomerService
    {
        Task AddAsync(ContractCustomer contract);
        Task<List<ContractCustomer>> GetByKontragentIdAsync(int kontragentId);
    }
}
