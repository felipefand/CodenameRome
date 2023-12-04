using Microsoft.AspNetCore.Mvc;
using CodenameRome.Models;
using CodenameRome.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CodenameRome.Dtos;

namespace CodenameRome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService) =>
            _productService = productService;


        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            var clientId = User.FindFirst(ClaimTypes.Name).Value;
            var productList = await _productService.GetAsync(clientId);
            return Ok(productList);
        }

        //[HttpGet("getcategories")]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //public async Task<IActionResult> GetCategories()
        //{
        //    var categories = await _menuService.GetCategoriesAsync();
        //    return Ok(categories);
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            if (product.ClientId != User.FindFirst(ClaimTypes.Name).Value) return Unauthorized();
            return Ok(product);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post(ProductDto product)
        {
            var newProduct = new Product();

            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.Description = product.Description;
            newProduct.Category = product.Category;
            newProduct.ClientId = User.FindFirst(ClaimTypes.Name).Value;

            await _productService.CreateAsync(newProduct);
            return Created(String.Empty, newProduct);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            if (product.ClientId != User.FindFirst(ClaimTypes.Name).Value) return Unauthorized();

            await _productService.RemoveAsync(id);
            return Ok(product);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> Update(string id, ProductDto product)
        {
            var oldProduct = await _productService.GetByIdAsync(id);

            if (oldProduct == null) return NotFound();
            if (oldProduct.ClientId != User.FindFirst(ClaimTypes.Name).Value) return Unauthorized();

            var updatedProduct = new Product();
            updatedProduct.Id = oldProduct.Id;
            updatedProduct.ClientId = oldProduct.ClientId;
            updatedProduct.Name = product.Name;
            updatedProduct.Price = product.Price;
            updatedProduct.Description = product.Description;
            updatedProduct.Category = product.Category;

            await _productService.UpdateAsync(id, updatedProduct);
            return Ok(updatedProduct);
        }
    }
}
