namespace webapp.Service.LoginService;
using Blazored.LocalStorage;
using core.Models;

public class LoginServiceClientSide : ILoginService  {
    
    private ILocalStorageService localStorage { get; set; }
    
    public LoginServiceClientSide(ILocalStorageService ls) {
        localStorage = ls;
    }
    public async Task<User?> GetUserLoggedIn() {
        var res = await localStorage.GetItemAsync<User>("user");
        return res;
    }
    public async Task<bool> Login(string username, string password) 
    {
        if (await Validate(username, password))
        {
            var user = new User 
            { 
                Email = username,
                Password = "verified",
                Roles = new List<UserRole> { UserRole.CoWorker }
            };
            
            await localStorage.SetItemAsync("user", user);
            return true;
        }
        return false;
    }

    protected virtual async Task<bool> Validate(string username, string password)
    {
        return username.Equals("peter") && password.Equals("1234");
    }

    public async Task<bool> IsLoggedIn()
    {
        var user = await GetUserLoggedIn();
        return user != null;
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("user");
    }
}
