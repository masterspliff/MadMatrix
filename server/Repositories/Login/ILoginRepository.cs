namespace server.Repositories.Login;

public interface ILoginRepository
{
    bool IsValid(User user);
}