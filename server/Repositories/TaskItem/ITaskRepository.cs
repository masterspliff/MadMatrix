using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public interface ITaskRepository : IRepository<TaskItem>
{
    // TaskItem-specific repository methods can be added here if needed
}
