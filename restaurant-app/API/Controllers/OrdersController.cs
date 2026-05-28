using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Domain.Constant;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class OrdersController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return null;
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return null;
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            return null;
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Waiter}")]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderDto update)
        {
            return null;
        }
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return null;
        }
    }
}
