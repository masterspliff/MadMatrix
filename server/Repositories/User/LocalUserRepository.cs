using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public class LocalUserRepository : IUserRepository
{
    private readonly LocalRepository<User> _user;

    public LocalUserRepository(LocalRepository<User> user)
    {
        _user = user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _user.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _user.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(User entity)
    {
        return await _user.CreateAsync(entity);
    }

    public async Task UpdateAsync(User entity)
    {
        await _user.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _user.DeleteAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var users = await GetAllAsync();
        return users.FirstOrDefault(u => u.Email == email);
    }
}
