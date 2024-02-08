using BookStoreDBFirst.Models;
using BookStoreDBFirst.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JobApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IUserService _userService;

        public AuthController(IUserService userService, JobApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _userService = userService;
            _context = context;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid user data");

            if (user.Role == "admin" || user.Role == "applicant")
            {
                Console.WriteLine("asd  "+user.Role);

                var isRegistered = await _userService.RegisterAsync(user);
                Console.WriteLine("status"+isRegistered);

                if (isRegistered)
                {
                    var customUser = new User
                    {
                        Username = user.Username,
                        Password = user.Password,
                        Role = user.Role,
                    };

                    // Add the customUser to the DbSet and save it
                    _context.Users.Add(customUser);
                    await _context.SaveChangesAsync();

                    return Ok(user);
                }
                //var identityUser = new IdentityUser
                //{
                //    UserName = user.Username,
                //    // Other user properties
                //};

                //// Create the user using UserManager
                //var result = await _userManager.CreateAsync(identityUser, user.Password);

                //if (result.Succeeded)
                //{
                //    // Assign roles to the user
                //    if (user.Role == "admin")
                //    {
                //        await _userManager.AddToRoleAsync(identityUser, "admin");
                //    }
                //    else if (user.Role == "applicant")
                //    {
                //        await _userManager.AddToRoleAsync(identityUser, "applicant");
                //    }

                //    return Ok("Registration successful");
                //}
            }

            return BadRequest("Registration failed. Username may already exist.");
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        //{
        //    Console.WriteLine("sdfg"+request.Username);
        //    if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        //        return BadRequest("Invalid login request");

        //    var token = await _userService.LoginAsync(request.Username, request.Password);

        //    if (token == null)
        //        return Unauthorized("Invalid username or password");

        //    return Ok(new { Token = token });
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Invalid login request");

            var token = await _userService.LoginAsync(request.Username, request.Password);

            if (token == null)
                return Unauthorized("Invalid username or password");

            // Retrieve the user from UserManager to get their roles
            var user = await _userManager.FindByNameAsync(request.Username);
            Console.WriteLine("role"+user);
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new { Token = token, Roles = roles });
        }


        [Authorize(Roles = "admin")]
        [HttpGet("admin")]
        public IActionResult AdminProtected()
        {
            return Ok("This is an admin-protected endpoint.");
        }

        [Authorize(Roles = "applicant")]
        [HttpGet("applicant")]
        public IActionResult applicantProtected()
        {
            return Ok("This is an applicant-protected endpoint.");
        }
    }
}
