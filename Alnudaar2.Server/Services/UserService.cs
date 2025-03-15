using Alnudaar2.Server.Models;
using Alnudaar2.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BCrypt.Net;

namespace Alnudaar2.Server.Services
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(User user);
        Task<User> LoginUserAsync(UserLoginDto userLoginDto);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            try
            {
                _logger.LogInformation("Registering user: {Username}", user.Username);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash the plain text password
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("User registered successfully: {Username}", user.Username);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Username}", user.Username);
                throw;
            }
        }

        public async Task<User> LoginUserAsync(UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLoginDto.Username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash)) // Verify the hashed password
                {
                    _logger.LogWarning("Invalid login attempt for username: {Username}", userLoginDto.Username);
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user: {Username}", userLoginDto.Username);
                throw;
            }
        }
    }
}