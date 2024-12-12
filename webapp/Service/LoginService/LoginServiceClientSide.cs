using Blazored.LocalStorage;
using System.Net.Http.Json;
using core.Models;

namespace webapp.Service.LoginService;

public class LoginServiceClientSide : ILoginService
{
    public event EventHandler AuthStateChanged;
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    
    public LoginServiceClientSide(ILocalStorageService localStorage, HttpClient http)
    {
        _localStorage = localStorage;
        _http = http;
    }

    public async Task<User?> GetCurrentUser()
    {
        return await _localStorage.GetItemAsync<User>("user");
    }

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

    public async Task<bool> IsLoggedIn()
    {
        var user = await GetCurrentUser();
        return user != null;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("user");
        await NotifyAuthStateChanged();
    }

    public async Task NotifyAuthStateChanged()
    {
        AuthStateChanged?.Invoke(this, EventArgs.Empty);
        await Task.CompletedTask;
    }
}
