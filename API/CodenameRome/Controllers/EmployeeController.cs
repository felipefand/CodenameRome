using CodenameRome.Application.Interfaces;
using CodenameRome.Contracts.Employees;
using CodenameRome.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodenameRome.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeApplication _employeeApplication;
        public EmployeeController(IEmployeeApplication employeeApplication)
        {
            _employeeApplication = employeeApplication;
        }

        [Authorize(Roles = "0, 10, 20")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetByClientId()
        {
            var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
            var employeeList = await _employeeApplication.GetEmployeesByClientId(clientId);

            return Ok(employeeList);
        }

        [Authorize(Roles = "0, 10, 20")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var employee = await _employeeApplication.GetEmployeeById(id, clientId);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "0, 10, 20")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Create(EmployeeDto employee)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var employeeRole = User.FindFirst(ClaimTypes.Role)!.Value;
                var newEmployee = await _employeeApplication.CreateEmployee(employee, clientId, employeeRole);

                return Created(String.Empty, newEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "0, 10, 20")]
        [HttpPut("/changepassword/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePassword(string id, string newPassword)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var employeeId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var employeeRole = User.FindFirst(ClaimTypes.Role)!.Value;

                await _employeeApplication.ChangePassword(clientId, employeeId, employeeRole, id, newPassword);


                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
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

        [Authorize(Roles = "0, 10, 20, 30")]
        [HttpPut("/changemypassword/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOwnPassword(ChangePassRequest changePasswordDto)
        {
            try
            {
                var employeeId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

                await _employeeApplication.ChangeOwnPassword(employeeId, changePasswordDto);

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "0, 10, 20")]
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, EmployeeDto employeeDto)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var employeeId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var employeeRole = User.FindFirst(ClaimTypes.Role)!.Value;

                var updatedEmployee = await _employeeApplication.UpdateEmployee(clientId, employeeId, employeeRole, id, employeeDto);

                return Ok(updatedEmployee);
            }
            catch(ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "0, 10, 20")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.Name)!.Value;
                var employeeRole = User.FindFirst(ClaimTypes.Role)!.Value;

                var employee = await _employeeApplication.DeleteEmployee(clientId, employeeRole, id);
                return Ok(employee);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
