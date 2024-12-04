using System.Net.Http.Json;
using core.Models;

namespace webapp.Service;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
        var response = await _httpClient.PostAsJsonAsync("user/login", loginDto);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<User>();
        }
        
        throw new Exception("Invalid email or password");
    }
}