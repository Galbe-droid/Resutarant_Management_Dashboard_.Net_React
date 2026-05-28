using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Template_restaurant_app.Application.Dtos.Payment;
using Template_restaurant_app.Domain.Constant;

namespace Template_restaurant_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class PaymentsController : ControllerBase
    {
        [Authorize(Roles = $"{Roles.Admin}")]
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
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Cashier}")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto create)
        {
            return null;
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Cashier}")]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdatePaymentDto update)
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
