using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface IEventRepository
{
    System.Threading.Tasks.Task<IEnumerable<Event>> GetAllAsync();
    System.Threading.Tasks.Task<Event?> GetByIdAsync(int id);
    System.Threading.Tasks.Task<Event> CreateAsync(Event entity);
    System.Threading.Tasks.Task UpdateAsync(Event entity);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
