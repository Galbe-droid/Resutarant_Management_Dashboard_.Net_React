using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Template_restaurant_app.Application.Dtos.User;
using Template_restaurant_app.Application.Interfaces;

namespace Template_restaurant_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _logger.LogInformation("Login attempt for user {Login}", login.Login);
            var token = await _authService.LoginAsync(login);

            if (token.Data == null)
            {
                _logger.LogWarning("Login failed for user {Login}", login.Login);
                return Unauthorized();
            }

            var userInfo = await _authService.GetUserAsync(login.Login);

            _logger.LogInformation("Login successful for user {Login}", login.Login);
            return Ok(new { token.Data, userInfo });
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _logger.LogInformation("Registration attempt for user {Username}", register.Username);
            var result = await _authService.RegisterAdminAsync(register);

            if (!result.Sucess)
            {
                _logger.LogWarning("Registration failed for user {Username}", register.Username);
                return BadRequest(new { errors = result.Errors });
            }

            _logger.LogInformation("Registration successful for user {Username}", register.Username);
            return Ok();
        }
        [Authorize]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody]TokenRequest request)
        {
            _logger.LogInformation("Token refresh attempt for user {UserId}", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _authService.RefreshAsync(request, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

            if (!result.Success)
            {
                _logger.LogWarning("Token refresh failed for user {UserId}", User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Unauthorized(result);
            }

            _logger.LogInformation("Token refresh successful for user {UserId}", User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(TokenRequest request)
        {
            _logger.LogInformation("Logout attempt for user {UserId}", User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _authService.LogoutAsync(request, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

            if (!result.Success)
            {
                _logger.LogWarning("Logout failed for user {UserId}", User.FindFirstValue(ClaimTypes.NameIdentifier));
                return BadRequest(result);
            }

            _logger.LogInformation("Logout successful for user {UserId}", User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _logger.LogInformation("Delete attempt for user {UserId} by user {RequestingUserId}", id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _authService.DeleteUserAsync(id);

            if (!result.Data)
            {
                _logger.LogWarning("Delete failed for user {UserId} by user {RequestingUserId}", id, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return BadRequest();
            }

            _logger.LogInformation("Delete successful for user {UserId} by user {RequestingUserId}", id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok();
        }
    }
}
