using core.Models;
using server.Data;
using MongoDB.Driver;

namespace server.Repositories.Login;

public class LoginRepositoryMongoDB : ILoginRepository
{
    private readonly MongoDbContext _context;

    public LoginRepositoryMongoDB(MongoDbContext context)
    {
        _context = context;
    }

    public User? ValidateUser(LoginDto loginRequest)
    {
        var user = _context.Users
            .Find(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password)
            .FirstOrDefault();

        if (user != null)
        {
            var now = DateTime.UtcNow;
            var update = Builders<User>.Update
                .Set(u => u.LastLogin, now)
                .Set(u => u.UpdatedAt, now);

            _context.Users.UpdateOne(u => u.Id == user.Id, update);
            user.LastLogin = now;
            user.UpdatedAt = now;
        }

        return user;
    }
}
