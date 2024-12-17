using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

/// <summary>
/// Repository interface for managing User entities in the database
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves all users from the database
    /// </summary>
    /// <returns>A collection of all users</returns>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<User?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="entity">The user entity to create</param>
    /// <returns>The created user with assigned ID</returns>
    Task<User> CreateAsync(User entity);

    /// <summary>
    /// Updates an existing user in the database
    /// </summary>
    /// <param name="entity">The user entity with updated information</param>
    Task UpdateAsync(User entity);

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The ID of the user to delete</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Associates multiple events with a user
    /// </summary>
    /// <param name="userId">The ID of the user to update</param>
    /// <param name="eventIds">List of event IDs to associate with the user</param>
    /// <exception cref="KeyNotFoundException">Thrown when the specified user is not found</exception>
    Task AddEventsToUserAsync(int userId, List<int> eventIds);
}
