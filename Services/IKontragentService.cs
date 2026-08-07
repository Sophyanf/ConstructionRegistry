using ConstructionRegistry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services
{
    public interface IKontragentService
    {
        Task<List<Kontragent>> GetAllAsync();
        Task<Kontragent?> GetByInnAsync(string inn);
        Task AddAsync(Kontragent kontragent);
    }
}
