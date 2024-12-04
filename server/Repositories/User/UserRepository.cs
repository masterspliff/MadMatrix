using core.Models; 
using MongoDB.Driver; 
using server.Data; 

namespace server.Repositories;

// Implements the IUserRepository interface, providing CRUD operations for User entities
public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users; // MongoDB collection for storing User documents

    // Constructor: Initializes the MongoDB User collection using the provided context
    public UserRepository(MongoDbContext context)
    {
        _users = context.Users; // Access the Users collection from the database context
    }

    // Retrieves all users from the collection
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        // MongoDB query to find all documents; `_ => true` is a shorthand for selecting everything
        return await _users.Find(_ => true).ToListAsync(); // Convert results to a list asynchronously
    }

    // Retrieves a single user by their ID
    public async Task<User?> GetByIdAsync(int id)
    {
        // Create a filter to match the ID field
        var filter = Builders<User>.Filter.Eq(e => e.Id, id); 
        // Find the first document matching the filter or return null if none exists
        return await _users.Find(filter).FirstOrDefaultAsync();
    }

    // Creates a new user in the collection
    public async Task<User> CreateAsync(User entity)
    {
        // Retrieve the last user (sorted by ID in descending order)
        var lastUser = await _users.Find(_ => true)
            .SortByDescending(u => u.Id) // Sort by ID in descending order
            .FirstOrDefaultAsync(); // Get the first result or null if none exist

        // Assign a new ID: the next integer after the last user's ID or 1 if the collection is empty
        entity.Id = (lastUser?.Id ?? 0) + 1;

        // Insert the new user document into the collection
        await _users.InsertOneAsync(entity);

        // Return the created user with the new ID
        return entity;
    }

    // Updates an existing user in the collection
    public async Task UpdateAsync(User entity)
    {
        // Create a filter to match the user's ID
        var filter = Builders<User>.Filter.Eq(e => e.Id, entity.Id);

        // Replace the document matching the filter with the updated entity
        await _users.ReplaceOneAsync(filter, entity);
    }

    // Deletes a user from the collection by their ID
    public async Task DeleteAsync(int id)
    {
        // Create a filter to match the user's ID
        var filter = Builders<User>.Filter.Eq(e => e.Id, id);

        // Delete the document matching the filter
        await _users.DeleteOneAsync(filter);
    }
}
