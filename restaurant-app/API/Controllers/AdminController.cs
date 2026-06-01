using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Template_restaurant_app.Application.Dtos.User;
using Template_restaurant_app.Domain.Constant;

namespace Template_restaurant_app.API.Controllers
{
    [Authorize(Roles = $"{Roles.Admin}")]
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class AdminController : ControllerBase
    {
        //User Management Endpoints
        [HttpPost]
        [Route("create-account")]
        public async Task<IActionResult> CreateRestaurantAccount([FromBody] RegisterRestaurantUser register)
        {
            return null;
        }
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return null;
        }
        [HttpGet]
        [Route("user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            return null;
        }
        [HttpDelete]
        [Route("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return null;
        }

        //Restaurant Metrics Endpoints
        [HttpGet]
        [Route("receipts")]
        public async Task<IActionResult> GetRestaurantReceipts()
        {
            return null;
        }
        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetRestaurantOrders()
        {
            return null;
        }
        [HttpGet]
        [Route("popular-items")]
        public async Task<IActionResult> GetRestaurantPopularItems()
        {
            return null;
        }
    }
}
