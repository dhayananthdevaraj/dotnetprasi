using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

[Route("auth/api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("login")]
    public async Task<ActionResult> GetUserByUsernameAndPassword([FromBody] LoginRequestModel request)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid Credentials" });
            }

            var token = GenerateToken(user.UserId);

            var responseObj = new
            {
                username = $"{user.FirstName} {user.LastName}",
                role = user.Role,
                token = token,
                userId = user.UserId
            };

            return Ok(responseObj);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("signup")]
    public async Task<ActionResult> AddUser([FromBody] User newUser)
    {
        try
        {
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Success" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("users")]
    public async Task<ActionResult> GetAllUsers()
    {
        try
        {
            var users = await _context.Users.Select(u => new { u.FirstName, u.LastName, u.Role, u.UserId }).ToListAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    private string GenerateToken(int userId)
    {
        // Implement your token generation logic here
        // You may use a library like JWT for token generation
        // Example: var token = JwtUtility.GenerateToken(userId);
        // Make sure to include the necessary NuGet packages for JWT handling
        return "your_generated_token";
    }
}
public class LoginRequestModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
