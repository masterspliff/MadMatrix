using MongoDB.Driver; 
using MongoDB.Bson; 
using core.Models; 

namespace server.Data; 

/// <summary>
/// Provides access to MongoDB collections and handles database connectivity
/// </summary>
public class MongoDbContext 
{
    /// <summary>
    /// The MongoDB database instance used for all operations
    /// </summary>
    private readonly IMongoDatabase _database;

    /// <summary>
    /// The MongoDB client used to establish and maintain the database connection
    /// </summary>
    private readonly IMongoClient _client;

    /// <summary>
    /// Initializes a new instance of the MongoDbContext class and establishes database connection
    /// </summary>
    /// <param name="configuration">The application configuration containing MongoDB connection details</param>
    /// <exception cref="Exception">Thrown when database connection cannot be established</exception>
    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB"); // Hardcoded because of azure deployement.
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        _client = new MongoClient(settings);
        _database = _client.GetDatabase("MadMatrix");
    }
    
    /// <summary>
    /// Gets the collection of TaskEvents from the database
    /// </summary>
    public IMongoCollection<TaskEvent> Events => _database.GetCollection<TaskEvent>("Events");

    /// <summary>
    /// Gets the collection of Locations from the database
    /// </summary>
    public IMongoCollection<Location> Locations => _database.GetCollection<Location>("Locations");

    /// <summary>
    /// Gets the collection of Users from the database
    /// </summary>
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

    /// <summary>
    /// Gets the collection of TaskItems from the database
    /// </summary>
    public IMongoCollection<TaskItem> Tasks => _database.GetCollection<TaskItem>("Tasks");
}
