using core.Models; 
using MongoDB.Driver; 
using server.Data; 

namespace server.Repositories;

/// <summary>
/// Handles persistence and retrieval of User entities in MongoDB database
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    /// MongoDB collection containing all user documents.
    /// Used to perform CRUD operations on user data.
    /// </summary>
    private readonly IMongoCollection<User> _users;

    /// <summary>
    /// Initializes a new repository with access to users collection
    /// </summary>
    /// <param name="context">Database context providing access to MongoDB collections</param>
    public UserRepository(MongoDbContext context)
    {
        _users = context.Users; // Access the Users collection from the database context
    }

    /// <summary>
    /// Retrieves all users from the database
    /// </summary>
    /// <returns>A list of all users</returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific user by ID
    /// </summary>
    /// <param name="id">ID of the desired user</param>
    /// <returns>User if found, otherwise null</returns>
    public async Task<User?> GetByIdAsync(int id)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, id);
        return await _users.Find(filter).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="entity">User object to be created</param>
    /// <returns>The created user with assigned ID</returns>
    public async Task<User> CreateAsync(User entity)
    {
        var lastUser = await _users.Find(_ => true)
            .SortByDescending(u => u.Id)
            .FirstOrDefaultAsync();

        entity.Id = (lastUser?.Id ?? 0) + 1;
        await _users.InsertOneAsync(entity);
        return entity;
    }

    /// <summary>
    /// Updates an existing user in the database
    /// </summary>
    /// <param name="entity">User object with updated values</param>
    public async Task UpdateAsync(User entity)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, entity.Id);
        await _users.ReplaceOneAsync(filter, entity);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">ID of the user to be deleted</param>
    public async Task DeleteAsync(int id)
    {
        var filter = Builders<User>.Filter.Eq(e => e.Id, id);
        await _users.DeleteOneAsync(filter);
    }

    /// <summary>
    /// Adds events to an existing user
    /// </summary>
    /// <param name="userId">ID of the user to be updated</param>
    /// <param name="eventIds">List of event IDs to be added to the user</param>
    /// <exception cref="KeyNotFoundException">If user with given ID is not found</exception>
    public async Task AddEventsToUserAsync(int userId, List<int> eventIds)
    {
        var user = await _users.Find(u => u.Id == userId).FirstOrDefaultAsync(); // Find the user by ID
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found."); // Throw exception if user not found
        }

        // Merge the new events with existing ones, ensuring no duplicates
        user.EventIds = user.EventIds.Union(eventIds).ToList(); 

        await _users.ReplaceOneAsync(u => u.Id == userId, user); // Update the user in the database
    }

}
