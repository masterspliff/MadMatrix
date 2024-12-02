using System.Text.Json;
using core.Models;

namespace server.Repositories.Base;

public abstract class LocalRepositoryBase<T> where T : class
{
    protected readonly List<T> _items = new();
    protected readonly string _localStoragePath;
    protected int _nextId = 1;

    protected LocalRepositoryBase(string entityName)
    {
        _localStoragePath = $"{entityName.ToLower()}.json";
        LoadFromFile();
    }

    protected void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_items);
        File.WriteAllText(_localStoragePath, json);
    }

    protected void LoadFromFile()
    {
        if (File.Exists(_localStoragePath))
        {
            var json = File.ReadAllText(_localStoragePath);
            var loadedItems = JsonSerializer.Deserialize<List<T>>(json) ?? new();
            _items.Clear();
            _items.AddRange(loadedItems);
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.FromResult(_items);
    }

    protected abstract int GetEntityId(T entity);
    protected abstract void SetEntityId(T entity, int id);
}
