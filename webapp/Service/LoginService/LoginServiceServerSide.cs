using Blazored.LocalStorage;
using System.Net.Http.Json;
using core.Models;
    
namespace webapp.Service.LoginService;

public class LoginServiceServerSide : LoginServiceClientSide
{
    private readonly HttpClient _http;
    
    public LoginServiceServerSide(ILocalStorageService ls, HttpClient http) : base(ls)
    {
        _http = http;
    }

    protected override async Task<bool> Validate(string username, string password)
    {
        var loginDto = new LoginDto 
        { 
            Email = username,
            Password = password 
        };
        
        var response = await _http.PostAsJsonAsync("api/Login", loginDto);
        return response.IsSuccessStatusCode;
    }
}
