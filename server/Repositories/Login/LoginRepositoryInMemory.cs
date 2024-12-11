using core.Models;

namespace server.Repositories.Login
{
    public class LoginRepositoryInMemory : ILoginRepository
    {
        private List<User> users = new()
        {
            new User { 
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com", 
                Password = "1234",
                PhoneNumber = "1234567890",
                Roles = new List<UserRole> { UserRole.Administrator }
            },
            new User { 
                Id = 2,
                FirstName = "Worker",
                LastName = "User",
                Email = "worker@example.com", 
                Password = "1234",
                PhoneNumber = "1234567891",
                Roles = new List<UserRole> { UserRole.CoWorker }
            },
            new User { 
                Id = 3,
                FirstName = "Manager",
                LastName = "User",
                Email = "manager@example.com", 
                Password = "1234",
                PhoneNumber = "1234567892",
                Roles = new List<UserRole> { UserRole.Manager }
            }
        };

        public User? ValidateUser(LoginDto loginRequest)
        {
            Console.WriteLine($"Validating user: {loginRequest.Email}");
            
            var user = users.FirstOrDefault(u => 
                u.Email == loginRequest.Email && 
                u.Password == loginRequest.Password);

            Console.WriteLine($"User found: {user != null}");
            
            // No timestamp updates needed
            return user;
        }
    }
}
