using core.Models;

namespace server.Repositories.Login;

/// <summary>
/// Provides user authentication operations
/// </summary>
public interface ILoginRepository
{
    /// <summary>
    /// Validates user credentials and updates login timestamp
    /// </summary>
    /// <param name="loginRequest">The login credentials to validate</param>
    /// <returns>The authenticated User if credentials are valid, null otherwise</returns>
    User? ValidateUser(LoginDto loginRequest);
}
