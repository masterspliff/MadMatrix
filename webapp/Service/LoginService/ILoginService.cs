using core.Models;
namespace webapp.Service.LoginService;

public interface ILoginService
{
    // If a user is logged in, the user will be returned.
    // If no user is logged in, null will be returned
    Task<User?> GetCurrentUser();
    
    // If user is valid the function will return true and the
    // user is set to be logged in.
    Task<bool> Login(string email, string password);

    // Check if a user is currently logged in
    Task<bool> IsLoggedIn();

    // Log out the current user
    Task Logout();
}
