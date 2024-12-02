using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public interface IEventRepository : IRepository<Event>
{
    // Event-specific repository methods can be added here if needed
}
