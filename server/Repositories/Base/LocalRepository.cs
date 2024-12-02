using core.Models;
using System.Reflection;

namespace server.Repositories.Base;

public class LocalRepository<T> : LocalRepositoryBase<T>, IRepository<T> where T : class
{
    public LocalRepository(string entityName) : base(entityName)
    {
    }

    public async Task<T> CreateAsync(T entity)
    {
        SetEntityId(entity, _nextId++);
        _items.Add(entity);
        SaveToFile();
        return await Task.FromResult(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = _items.FirstOrDefault(e => GetEntityId(e) == id);
        if (entity != null)
        {
            _items.Remove(entity);
            SaveToFile();
        }
        await Task.CompletedTask;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Task.FromResult(_items.FirstOrDefault(e => GetEntityId(e) == id));
    }

    public async Task UpdateAsync(T entity)
    {
        var id = GetEntityId(entity);
        var index = _items.FindIndex(e => GetEntityId(e) == id);
        if (index != -1)
        {
            _items[index] = entity;
            SaveToFile();
        }
        await Task.CompletedTask;
    }

    protected override int GetEntityId(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        return (int)(idProperty?.GetValue(entity) ?? 0);
    }

    protected override void SetEntityId(T entity, int id)
    {
        var idProperty = typeof(T).GetProperty("Id");
        idProperty?.SetValue(entity, id);
    }
}
