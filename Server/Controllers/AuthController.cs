using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Client.Helpers;
using SchoolApp.Server.Data;

using SchoolApp.Server.Models;

namespace SchoolApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            if (user == null)
                return new LoginResponse { Success = false, Message = "User not found." };

            // Verify hashed password
            if (!PasswordHelper.Verify(request.Password, user.PasswordHash, user.PasswordSalt))
                return new LoginResponse { Success = false, Message = "Incorrect password." };

            return new LoginResponse
            {
                Success = true,
                Message = "Login successful.",
                Role = user.Role,
                UserId = user.Id.ToString()
            };
        }

        public class LoginRequest
        {
            public string Username { get; set; } = "";
            public string Password { get; set; } = "";
        }
        public class LoginResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; } = "";
            public string? UserId { get; set; }
            public string? Role { get; set; }
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Missing username or password.");
            }

            // Check if username exists
            if (await _context.Users.AnyAsync(x => x.Username == request.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Generate hash + salt
            var (hash, salt) = PasswordHelper.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "Admin"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully!");
        }

    }
}
