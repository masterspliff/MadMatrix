using core.Models;

namespace server.Repositories.Login;

public interface ILoginRepository
{
    User? ValidateUser(LoginDto loginRequest);
}
