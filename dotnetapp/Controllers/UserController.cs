// UserController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private const string HardcodedJwtSecretKey = "your_hardcoded_secret_key"; // Replace with your actual secret key
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [HttpPost("auth/login")]
    public async Task<ActionResult> GetUserByUsernameAndPassword([FromBody] LoginRequestModel request)
    {
        try
        {
            var user = await _userService.GetUserByEmailAndPassword(request.Email, request.Password);

            if (user == null)
            {
                return Ok(new { message = "Invalid Credentials" });
            }

            var token = AuthService.GenerateToken(user.UserId);

            var responseObj = new
            {
                username = $"{user.UserName}",
                role = user.UserRole,
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

    [HttpPost("auth/register")]
    public async Task<ActionResult> AddUser([FromBody] User newUser)
    {
        try
        {
            var result = await _userService.AddUser(newUser);

            if (result)
            {
                return Ok(new { message = "Success" });
            }
            else
            {
                return StatusCode(500, new { message = "Failed to add user" });
            }
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
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
