using Blazored.LocalStorage;
using System.Net.Http.Json;
using core.Models;

namespace webapp.Service.LoginService;

/// <summary>
/// Client-side implementation of the ILoginService interface that manages user authentication
/// using local storage and HTTP requests
/// </summary>
public class LoginServiceClientSide : ILoginService
{
    /// <summary>
    /// Event that is triggered when the authentication state changes
    /// </summary>
    public event EventHandler AuthStateChanged;

    /// <summary>
    /// HTTP client for making authentication requests to the server
    /// </summary>
    private readonly HttpClient _http;

    /// <summary>
    /// Service for managing user data in local storage
    /// </summary>
    private readonly ILocalStorageService _localStorage;
    
    /// <summary>
    /// Initializes a new instance of the LoginServiceClientSide
    /// </summary>
    /// <param name="localStorage">The local storage service for managing user data</param>
    /// <param name="http">The HTTP client for making API requests</param>
    public LoginServiceClientSide(ILocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
    }

    /// <summary>
    /// Retrieves the currently logged-in user from local storage
    /// </summary>
    /// <returns>The current User object if one exists in local storage, null otherwise</returns>
    public async Task<User?> GetCurrentUser()
    {
        return await _localStorage.GetItemAsync<User>("user");
    }

    /// <summary>
    /// Attempts to authenticate a user with the provided credentials
    /// </summary>
    /// <param name="username">The user's email address</param>
    /// <param name="password">The user's password</param>
    /// <returns>True if authentication succeeds and user is stored in local storage, false otherwise</returns>
    public async Task<bool> Login(string username, string password)
    {
        var loginDto = new LoginDto 
        { 
            Email = username,
            Password = password 
        };
        
        var response = await _http.PostAsJsonAsync("user/login", loginDto);
        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadFromJsonAsync<User>();
            if (user != null)
            {
                await _localStorage.SetItemAsync("user", user);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if there is currently a user logged in by verifying local storage
    /// </summary>
    /// <returns>True if a user exists in local storage, false otherwise</returns>
    public async Task<bool> IsLoggedIn()
    {
        var user = await GetCurrentUser();
        return user != null;
    }

    /// <summary>
    /// Logs out the current user by removing their data from local storage
    /// and notifying subscribers of the change
    /// </summary>
    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("user");
        await NotifyAuthStateChanged();
    }

    /// <summary>
    /// Triggers the AuthStateChanged event to notify subscribers of authentication state changes
    /// </summary>
    public async Task NotifyAuthStateChanged()
    {
        AuthStateChanged?.Invoke(this, EventArgs.Empty);
        await Task.CompletedTask;
    }
}
