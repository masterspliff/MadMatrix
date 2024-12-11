using MongoDB.Driver; // Import MongoDB driver for database operations
using MongoDB.Bson; // Import Bson for MongoDB document representation
using core.Models; // Import models from the core project

namespace server.Data; // Define the namespace for the data context

public class MongoDbContext // Define the MongoDB context class
{
    private readonly IMongoDatabase _database; // MongoDB database instance
    private readonly IMongoClient _client; // MongoDB client instance

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

    public IMongoClient Client => _client; // Public property to access the MongoDB client
    public IMongoDatabase Database => _database; // Public property to access the MongoDB database
    public IMongoCollection<TaskEvent> Events => _database.GetCollection<TaskEvent>("Events"); // Access the "Events" collection
    public IMongoCollection<Location> Locations => _database.GetCollection<Location>("Locations"); // Access the "Locations" collection
    public IMongoCollection<User> Users => _database.GetCollection<User>("Users"); // Access the "Users" collection
    public IMongoCollection<core.Models.TaskItem> Tasks => _database.GetCollection<core.Models.TaskItem>("Tasks"); // Access the "Tasks" collection
}
