using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface IEventRepository
{
    System.Threading.Tasks.Task<IEnumerable<TaskEvent>> GetAllAsync();
    System.Threading.Tasks.Task<TaskEvent?> GetByIdAsync(int id);
    System.Threading.Tasks.Task<TaskEvent> CreateAsync(TaskEvent entity);
    System.Threading.Tasks.Task UpdateAsync(TaskEvent entity);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
