using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Security.Claims;
using Template_restaurant_app.Application.Dtos.Table;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Constant;

namespace Template_restaurant_app.API.Controllers
{
    [Authorize(Roles = $"{Roles.Admin},{Roles.Waiter}")]
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class RestaurantTablesController : ControllerBase
    {
        private readonly IRestaurantTableService _restaurantTableService;
        private readonly ILogger<RestaurantTablesController> _logger;

        public RestaurantTablesController(IRestaurantTableService restaurantTableService, ILogger<RestaurantTablesController> logger)
        {
            _restaurantTableService = restaurantTableService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _restaurantTableService.GetAllAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _restaurantTableService.GetByIdAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateTableDto create)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning("Failed Attempt of creating a table for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return BadRequest();
            }

            _logger.LogInformation("Attempt of creating a table for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _restaurantTableService.CreateAsync(create, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTableDto update)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Failed Attempt of updating a table for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return BadRequest();
            }

            _logger.LogInformation("Attempt of updating a table for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _restaurantTableService.UpdateAsync(id, update, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [HttpPut]
        [Route("reservation/{id}")]
        public async Task<IActionResult> Reservation(Guid id, [FromBody] ChangeTableStatusDto change)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Failed Attempt of reserving a table for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return BadRequest();
            }

            _logger.LogInformation("Attempt of reserving a table for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _restaurantTableService.ReservationAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), change));
        }
        [HttpPut]
        [Route("cancel-reservation/{id}")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            _logger.LogInformation("Attempt of canceling a reservation for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _restaurantTableService.CancelReservationAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Attempt of Deleting a table for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _restaurantTableService.DeleteAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
    }
}
