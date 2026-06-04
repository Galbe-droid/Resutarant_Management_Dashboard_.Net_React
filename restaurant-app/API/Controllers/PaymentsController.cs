using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Template_restaurant_app.Application.Dtos.Payment;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Application.Services;
using Template_restaurant_app.Domain.Constant;

namespace Template_restaurant_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentService _paymentService;
        public PaymentsController(ILogger<PaymentsController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("User {User} is retrieving all payments", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _paymentService.GetAllAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("User {User} is retrieving payment with id {PaymentId}", User.FindFirstValue(ClaimTypes.Name), id);
            return Ok(await _paymentService.GetByIdAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Cashier}")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto create)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of creating a Payment for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _paymentService.CreateAsync(create, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }

            _logger.LogWarning("Invalid model state for creating a Payment for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest(ModelState);
        }
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Cashier}")]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdatePaymentDto update)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of updating a Payment with id {PaymentId} for user {User}", id, User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _paymentService.UpdateAsync(id, update, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }

            _logger.LogWarning("Invalid model state for updating a Payment with id {PaymentId} for user {User}", id, User.FindFirstValue(ClaimTypes.Name));
            return BadRequest(ModelState);
        }
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Attempt of deleting a Payment with id {PaymentId} for user {User}", id, User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _paymentService.DeleteAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
    }
}
