using CodenameRome.Dtos;
using CodenameRome.Models;
using CodenameRome.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodenameRome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService) =>
            _customerService = customerService;

        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            var clientId = User.FindFirst(ClaimTypes.Name).Value;
            var customerList = await _customerService.GetAsync(clientId);
            return Ok(customerList);
        }

        [HttpGet("{phoneNumber}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int phoneNumber)
        {
            var customer = await _customerService.GetByPhoneNumberAsync(phoneNumber);

            if (customer == null) return NotFound();

            var clientId = User.FindFirst(ClaimTypes.Name).Value;
            if (!customer.CustomerFrom.Contains(clientId)) return Unauthorized();

            return Ok(customer);
        }
    }
}
