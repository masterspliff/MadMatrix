using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(MongoDbContext context)
    {
        _users = context.Users;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, id);
        return await _users.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User> CreateAsync(User entity)
    {
        await _users.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(User entity)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, entity.Id);
        await _users.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, id);
        await _users.DeleteOneAsync(filter);
    }
}
