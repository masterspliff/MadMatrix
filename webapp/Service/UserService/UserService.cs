using System.Net.Http.Json;
using core.Models;
using webapp.Service.LoginService;

namespace webapp.Service;

/// <summary>
/// Implementation of the IUserService interface that handles user operations
/// </summary>
public class UserService : IUserService
{
    /// <summary>
    /// HTTP client used for making requests to the user API endpoints
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Service for managing user authentication
    /// </summary>
    private readonly ILoginService _loginService;
    
    /// <summary>
    /// Initializes a new instance of the UserService with required dependencies
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API requests</param>
    /// <param name="loginService">The login service for authentication operations</param>
    public UserService(HttpClient httpClient, ILoginService loginService)
    {
        _httpClient = httpClient;
        _loginService = loginService;
    }

    public async Task<User> RegisterUser(RegisterDto registerDto)
    {
        var response = await _httpClient.PostAsJsonAsync("user/register", registerDto);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<User>();
        }
        
        var error = await response.Content.ReadAsStringAsync();
        throw new Exception($"Registration failed: {error}");
    }


    public async Task<User> LoginUser(LoginDto loginDto)
    {
        var success = await _loginService.Login(loginDto.Email, loginDto.Password);
        if (success)
        {
            return await _loginService.GetCurrentUser() ?? throw new Exception("Login failed");
        }
        throw new Exception("Invalid email or password");
    }
    
    public async Task<List<User>> LoadUsers()
    {
        List<User> UserList = new List<User>();
        
        var response = await _httpClient.GetAsync("user");
        if (response.IsSuccessStatusCode)
        {
            UserList = await response.Content.ReadFromJsonAsync<List<User>>();
            return UserList;
        }
        throw new Exception("Could not load task");
    }
    
    public async Task<User> GetUserById(int id)
    {
        var response = await _httpClient.GetAsync($"user/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<User>();
        }
        
        throw new Exception("User not found");
    }
    
    public async Task UpdateUser(User user)
    {
        var response = await _httpClient.PutAsJsonAsync($"user/{user.Id}", user);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Update failed: {error}");
        }
    }
    
    public async Task<bool> AddEventsToUserAsync(int userId, List<int> eventIds)
    {
        var response = await _httpClient.PutAsJsonAsync($"user/{userId}/add-events", eventIds);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to assign events to user {userId}: {response.StatusCode}");
        }

        return response.IsSuccessStatusCode;
    }
    
}
