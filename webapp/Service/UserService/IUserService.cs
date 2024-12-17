using core.Models;

namespace webapp.Service;

/// <summary>
/// Provides services for managing users in the system, including registration, authentication, and user data management
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user in the system
    /// </summary>
    /// <param name="registerDto">The registration information for the new user</param>
    /// <returns>The newly created User object</returns>
    /// <exception cref="Exception">Thrown when registration fails</exception>
    Task<User> RegisterUser(RegisterDto registerDto);

    /// <summary>
    /// Authenticates a user and creates their session
    /// </summary>
    /// <param name="loginDto">The login credentials</param>
    /// <returns>The authenticated User object</returns>
    /// <exception cref="Exception">Thrown when login fails or credentials are invalid</exception>
    Task<User> LoginUser(LoginDto loginDto);

    /// <summary>
    /// Retrieves all users from the system
    /// </summary>
    /// <returns>A list of all User objects</returns>
    /// <exception cref="Exception">Thrown when users cannot be loaded</exception>
    Task<List<User>> LoadUsers();

    /// <summary>
    /// Retrieves a specific user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <returns>The requested User object</returns>
    /// <exception cref="Exception">Thrown when the user is not found</exception>
    Task<User> GetUserById(int id);

    /// <summary>
    /// Updates an existing user's information
    /// </summary>
    /// <param name="user">The updated user information</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    /// <exception cref="Exception">Thrown when the update fails</exception>
    Task UpdateUser(User user);

    /// <summary>
    /// Associates multiple events with a specific user
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="eventIds">List of event IDs to associate with the user</param>
    /// <returns>True if the events were successfully added to the user, false otherwise</returns>
    Task<bool> AddEventsToUserAsync(int userId, List<int> eventIds);
}
