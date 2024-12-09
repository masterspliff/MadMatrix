using Blazored.LocalStorage;
using System.Net.Http.Json;
using core.Models;

namespace webapp.Service.LoginService;

public class LoginServiceClientSide : ILoginService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;
    private readonly LoginMode _mode;
    
    private readonly List<User> _demoUsers = new()
    {
        new User 
        { 
            Id = 1,
            FirstName = "Demo",
            LastName = "Admin",
            Email = "admin@demo.com",
            Password = "demo",
            Roles = new List<UserRole> { UserRole.Administrator }
        },
        new User 
        { 
            Id = 2,
            FirstName = "Demo",
            LastName = "Worker",
            Email = "worker@demo.com",
            Password = "demo",
            Roles = new List<UserRole> { UserRole.CoWorker }
        }
    };
    
    public LoginServiceClientSide(ILocalStorageService localStorage, HttpClient http, LoginMode mode = LoginMode.Online)
    {
        _localStorage = localStorage;
        _http = http;
        _mode = mode;
    }

    public async Task<User?> GetUserLoggedIn()
    {
        return await _localStorage.GetItemAsync<User>("user");
    }

    public async Task<bool> Login(string username, string password)
    {
        if (_mode == LoginMode.Demo)
        {
            return await DemoLogin(username, password);
        }
        return await OnlineLogin(username, password);
    }

    private async Task<bool> OnlineLogin(string username, string password)
    {
        var loginDto = new LoginDto 
        { 
            Email = username,
            Password = password 
        };
        
        var response = await _http.PostAsJsonAsync("api/Login", loginDto);
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

    private async Task<bool> DemoLogin(string username, string password)
    {
        var user = _demoUsers.FirstOrDefault(u => 
            u.Email == username && 
            u.Password == password);

        if (user != null)
        {
            await _localStorage.SetItemAsync("user", user);
            return true;
        }
        return false;
    }

    public async Task<bool> IsLoggedIn()
    {
        var user = await GetUserLoggedIn();
        return user != null;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("user");
    }
}
