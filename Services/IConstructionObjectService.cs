using ConstructionRegistry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services
{
    public interface IConstructionObjectService
    {
        Task<List<ConstructionObject>> GetAllWithCustomerAsync();
        Task AddAsync(ConstructionObject obj);
        Task RemoveAsync(int id);
    }
}
