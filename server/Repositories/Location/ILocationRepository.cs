using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface ILocationRepository
{
    System.Threading.Tasks.Task<IEnumerable<Location>> GetAllAsync();
    System.Threading.Tasks.Task<Location?> GetByIdAsync(int id);
    System.Threading.Tasks.Task<Location> CreateAsync(Location entity);
    System.Threading.Tasks.Task UpdateAsync(Location entity);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
