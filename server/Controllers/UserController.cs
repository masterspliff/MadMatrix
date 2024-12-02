using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Create a new User
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return BadRequest("Email already registered");

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password,  // Hashing the password
                Phone = registerDto.Phone,
                Role = registerDto.Role,
                EmailNotification = registerDto.EmailNotification,
                SmsNotification = registerDto.SmsNotification
            };

            await _userRepository.CreateAsync(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // Get a user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        // Update a user
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("User ID mismatch");

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            await _userRepository.UpdateAsync(user);
            return NoContent();
        }

        // Delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return NotFound();

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }

        // Login User
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null) 
                return Unauthorized("Invalid email or password");

            return Ok(user);
        }
    }
}
