using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllAsync();
    Task<Location?> GetByIdAsync(int id);
    Task<Location> CreateAsync(Location entity);
    Task UpdateAsync(Location entity);
    Task DeleteAsync(int id);
}
