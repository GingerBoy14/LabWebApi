using LabWebAPI.Contracts.DTO.AdminPanel;
using LabWebAPI.Contracts.DTO.Product;
using LabWebAPI.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
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
        private string UserId => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("products")]
        public async Task<IActionResult> EditUser([FromBody] ProductDTO model)
        {
            var result = await _productService.EditProductAsync(model);
            return Ok(result);
        }
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
