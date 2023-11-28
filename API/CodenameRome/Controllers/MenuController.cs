using Microsoft.AspNetCore.Mvc;
using CodenameRome.Models;
using CodenameRome.Services;

namespace CodenameRome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;
        public MenuController(MenuService menuService) =>
            _menuService = menuService;


        [HttpGet]
        [ProducesResponseType(typeof(List<MenuItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var list = await _menuService.GetAsync();
            return Ok(list);
        }

        [HttpGet]
        [Route("getCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _menuService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MenuItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _menuService.GetAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(MenuItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(MenuItem newItem)
        {
            await _menuService.CreateAsync(newItem);
            return Created(String.Empty, newItem);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(string id)
        {
            var item = await _menuService.GetAsync(id);
            if (item == null) return NotFound();

            await _menuService.RemoveAsync(id);
            return Ok(item);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(MenuItem), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update(string id, MenuItem updatedItem)
        {
            var item = await _menuService.GetAsync(id);
            if (item == null) return NotFound();

            updatedItem.Id = item.Id;
            await _menuService.UpdateAsync(id, updatedItem);
            return Ok(updatedItem);
        }
    }
}
