using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
