using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Application.Services;
using Template_restaurant_app.Domain.Constant;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;
        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }
        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAllAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _orderService.GetByIdAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto create)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of creating an Order for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _orderService.CreateAsync(create, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }
            _logger.LogInformation("Attempt of creating an Order for user {User} failed due to invalid model state", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpPut]
        [Route("updateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusDto update)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of updating an Order status for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _orderService.UpdateStatusAsync(id, update, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }
            _logger.LogInformation("Attempt of updating an Order status for user {User} failed due to invalid model state", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpPut]
        [Route("updateTable/{id}")]
        public async Task<IActionResult> UpdateTable(Guid id, [FromBody] UpdateOrderTableDto update)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of updating an Order table for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _orderService.UpdateTableAsync(id, update, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }
            _logger.LogInformation("Attempt of updating an Order table for user {User} failed due to invalid model state", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpPost]
        [Route("addOrderItems/{id}/items/")]
        public async Task<IActionResult> AddOrderItems(Guid id, [FromBody] ReceiveOrderItemDto item)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of adding items to an Order for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _orderService.AddOrderItemsAsync(id, item, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }
            _logger.LogInformation("Attempt of adding items to an Order for user {User} failed due to invalid model state", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpPut]
        [Route("UpdateOrderItems/{id}/items/{itemId}")]
        public async Task<IActionResult> AddOrderItems(Guid id, Guid itemId, [FromBody] int quantity)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of updating items in an Order for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _orderService.UpdateOrderItemsAsync(id, itemId, quantity, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }
            _logger.LogInformation("Attempt of updating items in an Order for user {User} failed due to invalid model state", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpDelete]
        [Route("removeOrderItems/{id}/items/{itemId}")]
        public async Task<IActionResult> RemoveOrderItems(Guid id, Guid itemId)
        {
            _logger.LogInformation("Attempt of removing items from an Order for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _orderService.RemoveOrderItemsAsync(id, itemId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Attempt of deleting an Order for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _orderService.DeleteAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
    }
}
