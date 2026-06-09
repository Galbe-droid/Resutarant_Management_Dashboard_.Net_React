using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Template_restaurant_app.Application.Dtos.User;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Application.Services;
using Template_restaurant_app.Domain.Constant;

namespace Template_restaurant_app.API.Controllers
{
    [Authorize(Roles = $"{Roles.Admin}")]
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        public AdminController(ILogger<AdminController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }
        //User Management Endpoints
        [HttpPost]
        [Route("create-account")]
        public async Task<IActionResult> CreateRestaurantAccount([FromBody] RegisterRestaurantUser register)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating restaurant account: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }
            var result = await _adminService.CreateRestaurantAccountAsync(register, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));

            if (!result.Sucess)
            {
                _logger.LogError("Failed to create restaurant account: {ErrorMessage}", User.FindFirstValue(ClaimTypes.Name));
                return BadRequest();
            }
            return Ok(new { message = "Restaurant account created successfully" });
        }
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _adminService.GetAllUsersAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)); 
            return Ok(result);
        }
        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _adminService.GetUserByIdAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _adminService.DeleteFromAdminUserAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)); 
            return Ok(result);
        }

        //Restaurant Metrics Endpoints
        [HttpGet]
        [Route("receipts")]
        public async Task<IActionResult> GetRestaurantReceipts()
        {
            var result = await _adminService.GetRestaurantReceiptsAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            return Ok(result);
        }
        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetRestaurantOrders()
        {
            var result = await _adminService.GetRestaurantOrdersAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            return Ok(result);
        }
        [HttpGet]
        [Route("popular-items")]
        public async Task<IActionResult> GetRestaurantPopularItems()
        {
            var result = await _adminService.GetRestaurantPopularItemsAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!));
            return Ok(result);
        }
    }
}
