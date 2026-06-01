using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Template_restaurant_app.Application.Dtos.Category;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Application.Services;
using Template_restaurant_app.Domain.Constant;

namespace Template_restaurant_app.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }
        [Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _categoryService.GetByIdAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }

        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto create)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of creating a Ccategory for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _categoryService.CreateAsync(create, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }

            _logger.LogWarning("Failed Attempt of creating a category for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }

        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto update)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Attempt of updating a category for user {User}", User.FindFirstValue(ClaimTypes.Name));
                return Ok(await _categoryService.UpdateAsync(id, update, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
            }

            _logger.LogWarning("Failed Attempt of updating a category for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return BadRequest();
        }

        [Authorize(Roles = $"{Roles.Admin}")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Attempt of deleting a category for user {User}", User.FindFirstValue(ClaimTypes.Name));
            return Ok(await _categoryService.DeleteAsync(id, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));
        }
    }
}
