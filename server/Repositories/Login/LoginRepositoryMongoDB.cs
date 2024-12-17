using core.Models;
using server.Data;
using MongoDB.Driver;

namespace server.Repositories.Login;

/// <summary>
/// MongoDB implementation of ILoginRepository for user authentication
/// </summary>
public class LoginRepositoryMongoDB : ILoginRepository
{
    /// <summary>
    /// MongoDB database context for accessing user data
    /// </summary>
    private readonly MongoDbContext _context;

    /// <summary>
    /// Initializes a new instance of the LoginRepositoryMongoDB
    /// </summary>
    /// <param name="context">The MongoDB database context</param>
    public LoginRepositoryMongoDB(MongoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Validates user credentials and updates login timestamp if successful
    /// </summary>
    /// <param name="loginRequest">The login credentials containing email and password</param>
    /// <returns>The authenticated User if credentials are valid, null otherwise</returns>
    public User? ValidateUser(LoginDto loginRequest)
    {
        var user = _context.Users
            .Find(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password)
            .FirstOrDefault();

        // After successful credential verification, update tracking timestamps
        if (user != null)
        {
            // Get current UTC timestamp
            var now = DateTime.UtcNow;
            
            // Update audit fields for tracking user activity:
            // - LastLogin: tracks when the user last authenticated successfully
            // - UpdatedAt: tracks when the user record was last modified
            var update = Builders<User>.Update
                .Set(u => u.LastLogin, now)
                .Set(u => u.UpdatedAt, now);

            // Update the user document in MongoDB
            _context.Users.UpdateOne(u => u.Id == user.Id, update);
            
            // Update the local user object to reflect the changes
            user.LastLogin = now;
            user.UpdatedAt = now;
        }

        return user;
    }
}
