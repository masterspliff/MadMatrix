using core.Models;
namespace webapp.Service.LoginService;

/// <summary>
/// Provides authentication and user management services for the application
/// </summary>
public interface ILoginService
{
    /// <summary>
    /// Event that is triggered when the authentication state changes
    /// </summary>
    event EventHandler AuthStateChanged;

    /// <summary>
    /// Retrieves the currently logged-in user
    /// </summary>
    /// <returns>The current User object if logged in, null otherwise</returns>
    Task<User?> GetCurrentUser();
    
    /// <summary>
    /// Authenticates a user with their email and password
    /// </summary>
    /// <param name="email">The user's email address</param>
    /// <param name="password">The user's password</param>
    /// <returns>True if authentication was successful, false otherwise</returns>
    Task<bool> Login(string email, string password);

    /// <summary>
    /// Checks if there is currently a user logged into the application
    /// </summary>
    /// <returns>True if a user is logged in, false otherwise</returns>
    Task<bool> IsLoggedIn();

    /// <summary>
    /// Logs out the current user and clears their session
    /// </summary>
    Task Logout();

    /// <summary>
    /// Notifies all subscribers that the authentication state has changed
    /// </summary>
    Task NotifyAuthStateChanged();
}
