using CodenameRome.Auth;
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
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly Hasher _hasher;
        public EmployeeController(EmployeeService employeeService, Hasher hasher)
        {
            _employeeService = employeeService;
            _hasher = hasher;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult>Get()
        {
            var clientId = User.FindFirst(ClaimTypes.Name).Value;
            var employeeList = await _employeeService.GetAsync(clientId);
            return Ok(employeeList);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post(EmployeeDto employee)
        {
            var newEmployee = new Employee();

            newEmployee.Name = employee.Name;
            newEmployee.Address = employee.Address;
            newEmployee.PhoneNumber = employee.PhoneNumber;
            newEmployee.Username = employee.Username;
            newEmployee.PasswordHash = _hasher.passwordTextToHash(employee.Password);
            newEmployee.Salary = employee.Salary;
            newEmployee.Role = employee.Role;
            newEmployee.ClientId = User.FindFirst(ClaimTypes.Name).Value;

            await _employeeService.CreateAsync(newEmployee);
            return Created(String.Empty, newEmployee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPassword(string id, string newPassword)
        {
            var employee = await _employeeService.GetByIdAsync(id);

            if (employee == null) return NotFound();
            if (employee.ClientId != User.FindFirst(ClaimTypes.Name).Value) return Unauthorized();

            var updatedEmployee = new Employee();
            updatedEmployee.Id = employee.Id;
            updatedEmployee.Name = employee.Name;
            updatedEmployee.Address = employee.Address;
            updatedEmployee.PhoneNumber = employee.PhoneNumber;
            updatedEmployee.Username = employee.Username;
            updatedEmployee.PasswordHash = _hasher.passwordTextToHash(newPassword);
            updatedEmployee.Salary = employee.Salary;
            updatedEmployee.Role = employee.Role;
            updatedEmployee.ClientId = employee.ClientId;

            await _employeeService.UpdateAsync(id, updatedEmployee);
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            if (employee.ClientId != User.FindFirst(ClaimTypes.Name).Value) return Unauthorized();

            await _employeeService.RemoveAsync(id);
            return Ok(employee);
        }
    }
}
