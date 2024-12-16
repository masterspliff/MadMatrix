using MongoDB.Driver; 
using MongoDB.Bson; 
using core.Models; 

namespace server.Data; 

public class MongoDbContext 
{
    private readonly IMongoDatabase _database; 
    private readonly IMongoClient _client;

    public MongoDbContext(IConfiguration configuration) // Constructor accepting configuration
    {
        var password = Environment.GetEnvironmentVariable("MONGODB_PASSWORD"); // Get MongoDB password from environment
        var baseConnectionString = configuration.GetConnectionString("MongoDB"); // Get base connection string from config
        var connectionString = baseConnectionString?.Replace("{password}", password); // Replace placeholder with actual password

        var settings = MongoClientSettings.FromConnectionString(connectionString); // Create settings from connection string
        settings.ServerApi = new ServerApi(ServerApiVersion.V1); // Set server API version

        _client = new MongoClient(settings); // Initialize MongoDB client
        _database = _client.GetDatabase("MadMatrix"); // Get the database named "MadMatrix"

        // Test connection
        try 
        {
            _database.RunCommand<BsonDocument>(new BsonDocument("ping", 1)); // Ping the database to test connection
            Console.WriteLine("Successfully connected to MongoDB MadMatrix database!"); // Log success message
        } 
        catch (Exception ex) 
        {
            Console.ForegroundColor = ConsoleColor.Red; // Set console text color to red
            Console.WriteLine($"MongoDB connection failed: {ex.Message}"); // Log failure message
            Console.ResetColor(); // Reset console text color
            throw; // Rethrow the exception
        }
    }
    
    public IMongoCollection<TaskEvent> Events => _database.GetCollection<TaskEvent>("Events"); // Access the "Events" collection
    public IMongoCollection<Location> Locations => _database.GetCollection<Location>("Locations"); // Access the "Locations" collection
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users"); // Access the "Users" collection
    public IMongoCollection<TaskItem> Tasks => _database.GetCollection<TaskItem>("Tasks"); // Access the "Tasks" collection
}
