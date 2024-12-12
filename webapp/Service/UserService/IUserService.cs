using core.Models;

namespace webapp.Service;

public interface IUserService                                                                                                                                    
{                                                                                                                                                                
    Task<User> RegisterUser(RegisterDto registerDto);                                                                                                            
    Task<User> LoginUser(LoginDto loginDto);                                                                                                                     
    Task<List<User>> LoadUsers();                                                                                                                                
    Task<User> GetUserById(int id);      
    Task UpdateUser(User user); 

}     
