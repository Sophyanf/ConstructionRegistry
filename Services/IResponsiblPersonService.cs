using ConstructionRegistry.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConstructionRegistry.Services
{
    public interface IResponsiblPersonService
    {
        Task<List<ResponsiblPerson>> GetAllByKontragentAsync(int kontragentId);
        Task AddAsync(ResponsiblPerson person);
    }
}
