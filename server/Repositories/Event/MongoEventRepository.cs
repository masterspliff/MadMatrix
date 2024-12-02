using core.Models;
using MongoDB.Driver;
using server.Repositories.Base;

namespace server.Repositories;

public class MongoEventRepository : IEventRepository
{
    private readonly MongoRepository<Event> _repository;

    public MongoEventRepository(MongoRepository<Event> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Event> CreateAsync(Event entity)
    {
        await _repository.CreateAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(Event entity)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, entity.Id);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
        await _repository.DeleteAsync(id);
    }
}
