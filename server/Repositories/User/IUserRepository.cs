using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}

/*
 * Example usage in a service:
 *
 * public class UserService
 * {
 *     private readonly IUserRepository _userRepository;
 *
 *     public UserService(IUserRepository userRepository)
 *     {
 *         _userRepository = userRepository;
 *     }
 *
 *     public async Task<User?> GetUserById(int userId)
 *     {
 *         return await _userRepository.GetByIdAsync(userId);
 *     }
 *
 *     public async Task<User> CreateUser(string name, string email)
 *     {
 *         var user = new User { Name = name, Email = email };
 *         return await _userRepository.CreateAsync(user);
 *     }
 *
 *     public async Task UpdateUserProfile(int userId, string name, string email)
 *     {
 *         var user = await _userRepository.GetByIdAsync(userId);
 *         if (user == null) throw new KeyNotFoundException($"User {userId} not found");
 *
 *         user.Name = name;
 *         user.Email = email;
 *         await _userRepository.UpdateAsync(user);
 *     }
 * }
 */
