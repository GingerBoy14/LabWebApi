using LabWebAPI.Contracts.Data.Entities;
using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Product;
using LabWebAPI.Contracts.Roles;
using LabWebAPI.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LabWebApi.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;


        public ProductController(IProductService productService, UserManager<User> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("products")]
        public async Task<IActionResult> EditProduct([FromBody] SimpleProductDTO model)
        {
            var result = await _productService.EditProductAsync(model);
            return Ok(result);
        }
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id, UserId);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] SimpleProductDTO model)
        {
            try
            {
            
                // Перевірте, чи користувач існує
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                // Створіть новий продукт
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    PublicationDate = model.PublicationDate,
                    UserWhoCreated = user // Пов'язання з поточним користувачем
                };

                // Додайте продукт до бази даних
                var result = await _productService.AddProduct(product);

                if (result)
                {
                    return Ok("Product created successfully");
                }
                else
                {
                    return BadRequest("Failed to create product");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
