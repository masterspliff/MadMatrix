using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("Users");
    }

    public async System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }
    
    public async System.Threading.Tasks.Task<User?> GetByEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Email, email);
        return await _users.Find(filter).FirstOrDefaultAsync();
    }

    public async System.Threading.Tasks.Task<User?> GetByIdAsync(int id)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, id);
        return await _users.Find(filter).FirstOrDefaultAsync();
    }

    public async System.Threading.Tasks.Task<User> CreateAsync(User entity)
    {
        await _users.InsertOneAsync(entity);
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(User entity)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, entity.Id);
        await _users.ReplaceOneAsync(filter, entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, id);
        await _users.DeleteOneAsync(filter);
    }
}
