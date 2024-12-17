using Microsoft.AspNetCore.Mvc;
using core.Models; 
using server.Repositories; 
namespace server.Controllers;

// Marks this class as an API controller and sets the base route for the endpoints
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    /// <summary>
    /// Repository for handling user data persistence and authentication
    /// </summary>
    private readonly IUserRepository _userRepository;

    // Constructor to inject the IUserRepository dependency
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Endpoint to retrieve all users
    /// <summary>
    /// Retrieves all users from the system
    /// </summary>
    /// <returns>A collection of all users, or an empty collection if none exist</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _userRepository.GetAllAsync(); // Fetch all users from the database
        return Ok(users); // Return the users with a 200 OK status
    }

    // Endpoint to retrieve a user by their ID
    /// <summary>
    /// Retrieves a specific user by their identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user to retrieve</param>
    /// <returns>The user if found, or NotFound if no user matches the given id</returns>
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
    /// <summary>
    /// Registers a new user in the system
    /// </summary>
    /// <param name="registerDto">The registration data including name, email, password, and other user details</param>
    /// <returns>The created user with assigned id if successful, or BadRequest if email already exists</returns>
    [HttpPost("register")]
    [Consumes("application/json")]
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
        };

        await _userRepository.CreateAsync(user); // Save the new user in the database

        // Return a 201 Created response, including the route to fetch the created user
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    // Endpoint to handle user login
    /// <summary>
    /// Authenticates a user with their credentials
    /// </summary>
    /// <param name="loginDto">The login credentials containing email and password</param>
    /// <returns>The authenticated user if successful, or Unauthorized if credentials are invalid</returns>
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

        await _userRepository.UpdateAsync(user); // Save the updated user details

        return Ok(user); // Return the user with a 200 OK status
    }

    // Endpoint to update an existing user
    /// <summary>
    /// Updates an existing user's information
    /// </summary>
    /// <param name="id">The id of the user to update</param>
    /// <param name="user">The updated user data</param>
    /// <returns>NoContent if successful, NotFound if user doesn't exist, or BadRequest if id mismatch</returns>
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

        await _userRepository.UpdateAsync(user); // Save the updated user details

        return NoContent(); // Return a 204 No Content status on successful update
    }

    // Endpoint to delete a user by their ID
    /// <summary>
    /// Deletes a user from the system
    /// </summary>
    /// <param name="id">The id of the user to delete</param>
    /// <returns>NoContent if successful, NotFound if user doesn't exist</returns>
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
    
    /// <summary>
    /// Associates multiple events with a specific user
    /// </summary>
    /// <param name="userId">The id of the user to update</param>
    /// <param name="eventIds">List of event IDs to associate with the user</param>
    /// <returns>NoContent if successful, NotFound if user doesn't exist, or BadRequest if validation fails</returns>
    [HttpPut("{userId}/add-events")]
    public async Task<IActionResult> AddEventsToUser(int userId, [FromBody] List<int> eventIds)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return bad request if the model state is invalid
        }

        try
        {
            // Call the repository method to add events to the user
            await _userRepository.AddEventsToUserAsync(userId, eventIds);

            return NoContent(); // Return 204 No Content if successful
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); // Return 404 if the user was not found
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user {userId}: {ex.Message}"); // Log the exception for debugging
            return StatusCode(500, "An error occurred while updating the user's events."); // Return 500 for internal errors
        }
    }


}
