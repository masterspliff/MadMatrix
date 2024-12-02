using MongoDB.Driver;
using MongoDB.Bson;
using core.Models;

namespace server.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IMongoClient _client;

    public MongoDbContext(IConfiguration configuration)
    {
        var password = Environment.GetEnvironmentVariable("MONGODB_PASSWORD");
        var baseConnectionString = configuration.GetConnectionString("MongoDB");
        var connectionString = baseConnectionString?.Replace("{password}", password);

        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _client = new MongoClient(settings);
        _database = _client.GetDatabase("MadMatrix");

        // Test connection
        try 
        {
            _database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Successfully connected to MongoDB MadMatrix database!");
        } 
        catch (Exception ex) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"MongoDB connection failed: {ex.Message}");
            Console.ResetColor();
            throw;
        }
    }

    public IMongoClient Client => _client;
    public IMongoDatabase Database => _database;
    public IMongoCollection<TaskEvent> Events => _database.GetCollection<TaskEvent>("Events");
    public IMongoCollection<Location> Locations => _database.GetCollection<Location>("Locations");
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<core.Models.TaskItem> Tasks => _database.GetCollection<core.Models.TaskItem>("Tasks");
}
