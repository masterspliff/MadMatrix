using core.Models;

namespace webapp.Service.LoginService;

public class LoginServiceInMemory : ILoginService
{
    private static readonly List<User> _demoUsers = new()
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
        },
        new User 
        { 
            Id = 3,
            FirstName = "Demo",
            LastName = "Manager",
            Email = "manager@demo.com",
            Password = "demo",
            Roles = new List<UserRole> { UserRole.Manager }
        }
    };

    public event EventHandler AuthStateChanged;

    public async Task<User?> GetCurrentUser()
    {
        // In memory service doesn't persist users between page refreshes
        return null;
    }

    public async Task<bool> IsLoggedIn()
    {
        var user = await GetCurrentUser();
        return user != null;
    }

    public async Task<bool> Login(string email, string password)
    {
        var user = _demoUsers.FirstOrDefault(u => 
            u.Email == email && 
            u.Password == password);
            
        return user != null;
    }

    public async Task Logout()
    {
        await NotifyAuthStateChanged();
    }

    public async Task NotifyAuthStateChanged()
    {
        AuthStateChanged?.Invoke(this, EventArgs.Empty);
        await Task.CompletedTask;
    }
}
