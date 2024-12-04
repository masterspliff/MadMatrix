using Microsoft.AspNetCore.Mvc;
using core.Models; 
using server.Repositories; 
namespace server.Controllers;

// Marks this class as an API controller and sets the base route for the endpoints
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository; // Dependency for interacting with the user database

    // Constructor to inject the IUserRepository dependency
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Endpoint to retrieve all users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _userRepository.GetAllAsync(); // Fetch all users from the database
        return Ok(users); // Return the users with a 200 OK status
    }

    // Endpoint to retrieve a user by their ID
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id); // Fetch a single user by ID
        if (user == null) // Check if the user exists
        {
            return NotFound(); // Return a 404 Not Found status if the user doesn't exist
        }
        return Ok(user); // Return the user with a 200 OK status
    }

    // Endpoint to register a new user
    [HttpPost("register")]
    [Consumes("application/json")] // Specifies that the endpoint expects JSON data
    public async Task<ActionResult<User>> Register([FromBody] RegisterDto registerDto)
    {
        var users = await _userRepository.GetAllAsync(); // Fetch all users to check for duplicate emails

        // Check if the email already exists in the database
        if (users.Any(u => u.Email == registerDto.Email))
        {
            return BadRequest("Email already exists"); // Return a 400 Bad Request status if email is duplicate
        }

        // Create a new User object using the data from the RegisterDto
        var user = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Password = registerDto.Password, // Password should ideally be hashed
            PhoneNumber = registerDto.PhoneNumber,
            Competencies = registerDto.Competencies, // Copy competencies from the DTO
            Roles = new List<UserRole> { UserRole.CoWorker }, // Assign a default role
            CreatedAt = DateTime.UtcNow, // Set creation timestamp
            UpdatedAt = DateTime.UtcNow  // Set update timestamp
        };

        await _userRepository.CreateAsync(user); // Save the new user in the database

        // Return a 201 Created response, including the route to fetch the created user
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    // Endpoint to handle user login
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginDto loginDto)
    {
        var users = await _userRepository.GetAllAsync(); // Fetch all users to check credentials
        var user = users.FirstOrDefault(u => 
            u.Email == loginDto.Email && // Match email
            u.Password == loginDto.Password); // Match password (should ideally use hashed comparison)

        if (user == null) // Check if credentials are valid
        {
            return Unauthorized("Invalid email or password"); // Return a 401 Unauthorized status if invalid
        }

        user.LastLogin = DateTime.UtcNow; // Update the last login timestamp
        await _userRepository.UpdateAsync(user); // Save the updated user details

        return Ok(user); // Return the user with a 200 OK status
    }

    // Endpoint to update an existing user
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user)
    {
        if (id != user.Id) // Ensure the ID in the URL matches the ID in the payload
        {
            return BadRequest(); // Return a 400 Bad Request status if mismatched
        }

        var existingUser = await _userRepository.GetByIdAsync(id); // Check if the user exists
        if (existingUser == null)
        {
            return NotFound(); // Return a 404 Not Found status if the user doesn't exist
        }

        user.UpdatedAt = DateTime.UtcNow; // Update the timestamp
        await _userRepository.UpdateAsync(user); // Save the updated user details

        return NoContent(); // Return a 204 No Content status on successful update
    }

    // Endpoint to delete a user by their ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userRepository.GetByIdAsync(id); // Fetch the user to be deleted
        if (user == null) // Check if the user exists
        {
            return NotFound(); // Return a 404 Not Found status if the user doesn't exist
        }

        await _userRepository.DeleteAsync(id); // Delete the user from the database
        return NoContent(); // Return a 204 No Content status on successful deletion
    }
}
