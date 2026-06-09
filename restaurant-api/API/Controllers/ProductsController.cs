using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Template_restaurant_app.Application.Dtos.Product;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Application.Services;
using Template_restaurant_app.Domain.Constant;

namespace Template_restaurant_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;
        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _productService.GetByIdAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto create)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of creating a Product for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _productService.CreateAsync(create, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }
            _logger.LogWarning("Failed attempt of creating a Product for user {User} with data", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto update)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of updating a Product for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _productService.UpdateAsync(id, update, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }
            _logger.LogWarning("Failed attempt of updating a Product for user {User} with data", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }
        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _productService.DeleteAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
    }
}
