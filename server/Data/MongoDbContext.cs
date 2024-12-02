using MongoDB.Driver;
using core.Models;

namespace server.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Event> Events => _database.GetCollection<Event>("Events");
    public IMongoCollection<Location> Locations => _database.GetCollection<Location>("Locations");
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<core.Models.Task> Tasks => _database.GetCollection<core.Models.Task>("Tasks");
}
