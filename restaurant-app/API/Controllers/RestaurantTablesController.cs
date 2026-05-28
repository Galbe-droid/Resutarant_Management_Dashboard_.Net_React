using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Template_restaurant_app.Application.Dtos.Table;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Constant;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Template_restaurant_app.API.Controllers
{
    [Authorize(Roles = $"{Roles.Admin},{Roles.Waiter}")]
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class RestaurantTablesController : ControllerBase
    {
        private readonly IRestaurantTableService _restaurantTableService;

        public RestaurantTablesController(IRestaurantTableService restaurantTableService)
        {
            _restaurantTableService = restaurantTableService;
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
                return BadRequest();
            }

            return Ok(await _restaurantTableService.CreateAsync(create, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTableDto update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _restaurantTableService.UpdateAsync(id, update, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [HttpPut]
        [Route("reservation/{id}")]
        public async Task<IActionResult> Reservation(Guid id, [FromBody] ChangeTableStatusDto change)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _restaurantTableService.ReservationAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), change));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _restaurantTableService.DeleteAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
    }
}
