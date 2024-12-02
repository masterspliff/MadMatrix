using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface ITaskRepository
{
    System.Threading.Tasks.Task<IEnumerable<TaskItem>> GetAllAsync();
    System.Threading.Tasks.Task<TaskItem?> GetByIdAsync(int id);
    System.Threading.Tasks.Task<TaskItem> CreateAsync(TaskItem entity);
    System.Threading.Tasks.Task UpdateAsync(TaskItem entity);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
