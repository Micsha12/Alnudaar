using Microsoft.AspNetCore.Mvc;
using Alnudaar2.Server.Models;
using Alnudaar2.Server.Services;
using Alnudaar2.Server.Data; // Add this line if ApplicationDbContext is in the Data namespace
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Alnudaar2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public UsersController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            // Normalize input
            user.Email = user.Email?.Trim().ToLower();
            user.Username = user.Username?.Trim();

            // Email format validation
            if (string.IsNullOrWhiteSpace(user.Email) ||
                !Regex.IsMatch(user.Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                return BadRequest("Invalid email format.");
            }

            // Email uniqueness check (case-insensitive)
            if (await _context.Users.AnyAsync(u => u.Email.ToLower() == user.Email))
            {
                return BadRequest("Email is already in use.");
            }

            // Username uniqueness check (case-insensitive)
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == user.Username.ToLower()))
            {
                return BadRequest("Username is already in use.");
            }

            try
            {
                var createdUser = await _userService.RegisterUserAsync(user);
                return CreatedAtAction(nameof(Register), new { id = createdUser.UserID }, createdUser);
            }
            catch (DbUpdateException ex)
            {
                // Handle rare race condition where two requests pass the check at the same time
                return Conflict("A user with this email or username already exists.");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDto userLoginDto)
        {
            var user = await _userService.LoginUserAsync(userLoginDto);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}