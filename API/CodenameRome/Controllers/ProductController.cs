using Microsoft.AspNetCore.Mvc;
using CodenameRome.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CodenameRome.Dtos;
using CodenameRome.Application.Interfaces;

namespace CodenameRome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "0, 10, 20")]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;
        public ProductController(IProductApplication productApplication) =>
            _productApplication = productApplication;

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByClientId()
        {
            var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
            var productList = await _productApplication.GetProductsByClientId(clientId);

            return Ok(productList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var product = await _productApplication.GetProductById(id, clientId);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(ProductDto product)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var newProduct = await _productApplication.CreateProduct(product, clientId);

                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(ProductDto product, string id)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var updatedProduct = await _productApplication.UpdateProduct(product, clientId, id);

                return Ok(updatedProduct);
            } 
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;

                var deletedProduct = await _productApplication.DeleteProduct(clientId, id);
                return Ok(deletedProduct);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
